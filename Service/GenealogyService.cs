

using System;
using System.Net.Http;
using System.Threading.Tasks;
using historianproductionservice.Model;
using historianproductionservice.Model.Genealogy;
using historianproductionservice.Service.Interface;
using Microsoft.Extensions.Configuration;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using historianproductionservice.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Web;

namespace historianproductionservice.Service{
    public class GenealogyService : IGenealogyService
    {
        private IConfiguration _configuration;
        private UriBuilder builder;
        private HttpClient client = new HttpClient();
        private readonly IProductionOrderService _productionOrderService;
        private ApplicationDbContext _context;
        private IOrderService _orderService;
        public GenealogyService(IConfiguration configuration, IProductionOrderService productionOrderService,  ApplicationDbContext context, IOrderService orderService){
            _configuration = configuration;
            _productionOrderService = productionOrderService;
            _context =   context;
            _orderService = orderService;
        }

        public async Task<List<Genealogy>> getByOp(string op){
            return _context.Genealogy.Include(x => x.outputRolls).ThenInclude(outputRolls => outputRolls.inputRolls)
                .Include(x => x.outputRolls).ThenInclude(outputRolls => outputRolls.ligas).ThenInclude(ligas=>ligas.productsInput)
                .Include(x => x.outputRolls).ThenInclude(outputRolls => outputRolls.tools).Where(x => x.orderId.ToString() == op).ToList();
        }

        public async Task<List<Genealogy>> getByCode(long? startdate, long? endDate, string cod){
            return _context.Genealogy.Include(x => x.outputRolls).ThenInclude(outputRolls => outputRolls.inputRolls).Include(x => x.outputRolls)
                .ThenInclude(outputRolls => outputRolls.ligas).ThenInclude(ligas=>ligas.productsInput).Include(x => x.outputRolls)
                .ThenInclude(outputRolls => outputRolls.tools).Where(x => x.startDate > startdate && x.startDate < endDate && cod == x.recipeid).ToList();
        }

        public async Task<List<Genealogy>> getByDate(long? startdate, long? endDate){
            return _context.Genealogy.Include(x => x.outputRolls).ThenInclude(outputRolls => outputRolls.inputRolls).Include(x => x.outputRolls)
                .ThenInclude(outputRolls => outputRolls.ligas).ThenInclude(ligas=>ligas.productsInput).Include(x => x.outputRolls)
                .ThenInclude(outputRolls => outputRolls.tools).Where(x => x.startDate > startdate && x.startDate < endDate).ToList();
        }

        public async Task<string> addEndRoll(InputData inputData){                             
            try{                
                List<Genealogy> gs = await getByOp(inputData.productionOrderId.ToString());                
                Console.WriteLine("Passou pela instancia de g");                            
                Genealogy g;bool v = false;
                //Verifica se existe genealogy cadastrada na base de dados !               
                if(gs.Count <= 0){
                    v = true;
                    g = await instanciaG(inputData);
                    Console.WriteLine("Add");                    
                }else{
                    Console.WriteLine("Update");
                    g = gs[0];                    
                }
                EndRoll e = await instanciaEndRoll(inputData, g);
                List<ProductTraceability> productsInput = await geraListas((int)g.orderId);
                List<ProductTraceability> productsInputLiga = productsInput.Where(c => c.productType=="liga").ToList();                
                List<ProductTraceability> productsInputRolo = productsInput.Where(c => c.productType=="aco").ToList();                                   
                productsInputLiga = await getProducts(e.startDate, e.endDate, productsInputLiga);                                                                
                productsInputRolo = await getProducts(e.startDate, e.endDate, productsInputRolo);                                
                var (listaRolo, listaLiga) = await criaListas(productsInputLiga, productsInputRolo);                  
                e.ligas =listaLiga; e.inputRolls = listaRolo;                                                        
                e.startDate = e.startDate == null ? g.startDate : e.startDate;
                e.tools = await getTools((long)e.startDate, e.endDate); g.outputRolls.Add(e);                                                
                if(v)                        
                    _context.Genealogy.Add(g);
                else
                    _context.Genealogy.Update(g);                      
                _context.SaveChanges();
                sendRollPLC(g.orderId);
                return "true";
            }catch(ArgumentException e){
                return e.Message +"\n "+ e.ParamName;
            }            
        }

        public async void sendRollPLC(long id){
            builder = new UriBuilder(_configuration["interlevelAPI"]);                     
            Console.WriteLine(await client.PostAsync(builder.Uri,new StringContent("{\"address\": \"Numero_ROLO\",\"value\":" + ( _context.EndRoll.Where(x=> x.productionOrderId == id).Count()+1).ToString() +",\"workstation\": \"Linha\"}")));                
        }
        public async Task<Genealogy> instanciaG(InputData inputData){
            Genealogy g = new Genealogy();
            var productionOrder = await _productionOrderService.getProductionOrder(inputData.productionOrderId);
            builder = new UriBuilder(_configuration["productionOrdersServiceEndpoint"]+"/api/productionorders/"+inputData.productionOrderId);                        
            Recipe r = JsonConvert.DeserializeObject<Recipe>(JObject.Parse((await client.GetStringAsync(builder.ToString())))["recipe"].ToString());                
            Console.WriteLine(r.recipeId);                        
            g = new Genealogy(inputData.productionOrderId,productionOrder.productionOrderNumber, (await getStartDateRoll(productionOrder.productionOrderId)), DateTime.Now.Ticks, new List<EndRoll>(), r.recipeCode, r.recipeId.ToString()); g.endDate = DateTime.Now.Ticks;                                                                                                    
            return g;
        }

        public async Task<List<ProductTraceability>> geraListas(int id){
            Order o = await _orderService.getProductionOrderId((int)id);              
            if(o==null)
                Console.WriteLine("O == null");            
            if(o == null || o.productsInput == null || o.productsInput.Count == 0)
                throw new System.ArgumentException("Não foram encontrados rolos de entrada", "Por favor cadastre o aço correspondente");
            List<ProductTraceability> lista = o.productsInput;
            return lista;
        }

        public async Task<EndRoll> instanciaEndRoll(InputData inputData, Genealogy g){
            EndRoll e = new EndRoll();
            e.endDate = DateTime.Now.Ticks; e.productionOrderId = inputData.productionOrderId;
            e.startDate = _context.EndRoll.Where(x=> x.productionOrderId == g.orderId).Max(p => (long?)p.endDate);
            if(e.startDate == null)
                e.startDate = g.startDate;
            e.quantity = inputData.quantity.ToString();    
            return e;
        }

        public async Task<List<Tool>> getTools(long startDate, long endDate){
            builder = new UriBuilder(_configuration["historianBigTable"]+"?thingId=1&startDate="+startDate+"&endDate="+(startDate+3600000000)); //startDate+600000000));
            Console.WriteLine(builder.ToString());
            if((await client.GetAsync(builder.ToString())).StatusCode != HttpStatusCode.OK)
                return null;   
            Console.WriteLine("Passou pela big ");                
            List<Tag> tags = JsonConvert.DeserializeObject<List<Tag>>(JObject.Parse(await client.GetStringAsync(builder.ToString()))["tags"].ToString());                         
            List<Tag> toolIds = tags.Where(c => c.name == "toolId" && c.value.First()!=null && c.value.First()!="0" && c.value.First()!="").ToList();                                           
            List<Tag> vida_utils = tags.Where(c => c.name == "vida_util").ToList();  /*Limpando memoria*/ tags = null;                                    
            string [] vetTags = new string[toolIds.Count()];int i=0;                        
            foreach(Tag t in toolIds)                
                vetTags[i++] = t.value.First();                 
            List<Tool> tools = null;                                    
            builder = new UriBuilder(_configuration["tool"]+"ids?ids="+string.Join(",", vetTags));
            Console.WriteLine(builder.ToString());
            if((await client.GetAsync(builder.ToString())).StatusCode == HttpStatusCode.OK)
                tools = JsonConvert.DeserializeObject<List<Tool>>(await client.GetStringAsync(builder.ToString()+string.Join(",", vetTags)));                                         
            foreach(Tool t in tools) 
                t.group = toolIds.Where(c => c.value.Contains(t.toolId)).First().group;                                                                                
            foreach(Tool t in tools)             
                t.vidaUtil = vida_utils.Where(x => x.group == t.group).FirstOrDefault().value.First();
            return tools;
        }

        public async Task<(List<Aco>,List<Liga>)> criaListas(List<ProductTraceability> ligas, List<ProductTraceability> rolo){            
            List<Aco> acos = new List<Aco>(); 
            List<Liga> ligasList = new List<Liga>();
            foreach(ProductTraceability r in rolo)
                acos.Add(new Aco(r.quantity.ToString(), r.batch, r.date, r.endDate));                
            foreach(ProductTraceability p in ligas){          
                if((await _orderService.getProductionOrderId(Int32.Parse(p.code))) == null)
                    throw new System.ArgumentException("Uma ou mais ligas sem apontamentos" , "Por favor cadastre o apontamento!");    
                ligasList.Add(new Liga(Convert.ToInt64(p.code), p.batch, p.date, p.endDate, p.code, p.quantity.ToString(), p.batch, (await _orderService.getProductionOrderId(Int32.Parse(p.code))).productsInput));                            
            }
            return (acos, ligasList);
        }
        


        public async Task<List<ProductTraceability>> getProducts(long? startDate, long? endDate, List<ProductTraceability> produtos){
            if(produtos.Count == 0 )
                throw new System.ArgumentException("Não foram encontrados rolos de entrada ou ligas", "Por favor cadastre o produto correspondente");
            produtos.Sort((x,y) => x.date.CompareTo(y.date));                         
            for(int i=0; i<produtos.Count-1; i++)
                produtos[i].endDate = produtos[i+1].date;                                         
            produtos[produtos.Count-1].endDate = endDate.Value;            
            List<ProductTraceability> retorno = new List<ProductTraceability>();
            retorno.AddRange(produtos.Where(c => c.date >= startDate && c.endDate <= endDate).ToList());            
            Console.WriteLine(retorno.Count);
            if(retorno.Count == 0)
                retorno.Add(produtos.Where(c => c.date < endDate).OrderByDescending(c=> c.date).FirstOrDefault());                            
            if(retorno.Count == 0)
                throw new System.ArgumentException("Não foram encontrados rolos de entrada", "Por favor cadastre o aço correspondente");
            return retorno;
        }

        public async Task<long> getStartDateRoll(long id){
            builder = new UriBuilder(_configuration["productionOrdersHistStates"]+"?ProductionOrderId="+id);                                
            if((await client.GetAsync(builder.ToString())).StatusCode != HttpStatusCode.OK)
                throw new System.ArgumentException("Parameter not found", "Production Order corrupted");                
            var states = JsonConvert.DeserializeObject<List<HistState>>(await client.GetStringAsync(builder.ToString()));             
            HistState state = states.Where(x => x.state == "active").FirstOrDefault();            
            Console.WriteLine(builder.ToString());
            return (state.date);
        }
               
    }
}