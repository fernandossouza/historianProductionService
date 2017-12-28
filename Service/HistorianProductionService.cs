using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using historianproductionservice.Service.Interface;
using historianproductionservice.Model;
using historianproductionservice.Data;


namespace historianproductionservice.Service
{
    public class HistorianProductionService : IProductsService

    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IOrderService _orderService;
        public HistorianProductionService(IConfiguration configuration, ApplicationDbContext context
        , IOrderService orderService)
        {
            _context =   context;
            _configuration = configuration;
            _orderService = orderService;
        }

         public async Task<InputData> addProduct(InputData inputData)
         {
            Product product = null;
            bool checkRecipe = false;
             // Faz get na receita
            string OrderRecipe = await GetProductionOrderApi(inputData.productionOrderId);

            if(OrderRecipe == null)
            {
                return null;
            }

            // Verifica tipo de processo input ou output
            switch (inputData.type)
            {
                case typeEnum.Input:
                    checkRecipe = CheckProductInRecipe(inputData.productId,OrderRecipe);
                    break;

                case typeEnum.Output:
                    checkRecipe = CheckProductOutRecipe(inputData.productId,OrderRecipe);
                    break;

            }

            // Verifica se o produto está na receita
             if(!checkRecipe)
             {
                 return null;
             }

             product = CreateProduct(inputData);

             product = await AddInDB(product,inputData);

             return inputData;
            

         }
        
         private async Task<Product> AddInDB(Product product, InputData inputData)
         {
             //verifica se a ordem já está cadastrada no banco
             Order order = await _orderService.getProductionOrderId(inputData.productionOrderId);

             if(order ==  null)
             {
                 Order orderNew = new Order();
                 order.productionOrderId = inputData.productionOrderId;
                 
                 order = await _orderService.addOrder(orderNew);
             }

             switch (inputData.type)
             {
                 case typeEnum.Input:
                 order.productsInput.Add(product);
                 break;

                 case typeEnum.Output:
                 order.productsOutput.Add(product);
                 break;                 
             }

             await _orderService.updateOrder(order.id,order);

             return product;             
         }

         private Product CreateProduct(InputData inputData)
         {
            Product product = new Product();

            product.productId = inputData.productId;
            product.batch = inputData.batch;
            product.quantity = inputData.quantity.Value;
            product.date = DateTime.Now.Ticks;

            return product;
         }

         private async Task<string> GetProductionOrderApi(int orderId)
         {
            HttpClient client = new HttpClient();
            string OrderRecipe = string.Empty;
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var builder = new UriBuilder(_configuration["recipeServiceEndpoint"] + "/api/productionorders/" + orderId);
            string url = builder.ToString();
            var result = await client.GetAsync(url);
            if(result.StatusCode == HttpStatusCode.OK)
            {
                    OrderRecipe = (await client.GetStringAsync(url));                    
                    return OrderRecipe;
            }
            return null;
         }

         private bool CheckProductInRecipe(int ProductId, string OrderRecipe)
         {
             JObject recipeJson = JObject.Parse(OrderRecipe);
             var productCount =from r in recipeJson["recipe"]["phases"]["phaseProducts"]
                                                    .Where(i =>  (int)i["productId"] == ProductId)
                                                    .SelectMany(i => i["product"]).Values<string>()
                                group r by r
                                into p
                                select new { Category = p.Key, Count = p.Count() };
            
            if(productCount.Count() >0)
            {
                return true;
            }
            return false;
         }

         private bool CheckProductOutRecipe(int ProductId, string OrderRecipe)
         {
             JObject recipeJson = JObject.Parse(OrderRecipe);
             var productCount =from r in recipeJson["recipe"]["recipeProduct"]
                                                    .Where(i =>  (int)i["productId"] == ProductId)
                                                    .SelectMany(i => i["product"]).Values<string>()
                                group r by r
                                into p
                                select new { Category = p.Key, Count = p.Count() };
            
            if(productCount.Count() >0)
            {
                return true;
            }
            return false;
         }
        
    }
}