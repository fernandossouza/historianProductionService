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
    public class ProductService : IProductsService

    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IOrderService _orderService;
        public ProductService(IConfiguration configuration, ApplicationDbContext context
        , IOrderService orderService)
        {
            _context = context;
            _configuration = configuration;
            _orderService = orderService;
        }

        public async Task<InputData> addProduct(InputData inputData)
        {
            ProductTraceability product = null;
            bool checkRecipe = false;
            string ProductName = string.Empty;
            string ProductionOrderName = string.Empty;
            // Faz get na receita
            string OrderRecipe = await GetProductionOrderApi(inputData.productionOrderId);

            if (OrderRecipe == null)
            {
                return null;
            }

            // Verifica tipo de processo input ou output
            switch (inputData.type)
            {
                case typeEnum.Input:
                    (checkRecipe, ProductName, ProductionOrderName) = CheckProductInRecipe(inputData.productId, OrderRecipe);
                    break;

                case typeEnum.Output:
                    (checkRecipe, ProductName, ProductionOrderName) = CheckProductOutRecipe(inputData.productId, OrderRecipe);
                    break;

            }

            // Verifica se o produto está na receita
            if (!checkRecipe)
            {
                return null;
            }

            product = CreateProduct(inputData, ProductName);

            product = await AddInDB(product, inputData, ProductionOrderName);

            return inputData;


        }

        private async Task<ProductTraceability> AddInDB(ProductTraceability product, InputData inputData, string ProductionOrderName)
        {
            //verifica se a ordem já está cadastrada no banco
            Order order = await _orderService.getProductionOrderId(inputData.productionOrderId);

            if (order == null)
            {
                Order orderNew = new Order();
                orderNew.productionOrderId = inputData.productionOrderId;
                orderNew.order = ProductionOrderName;
                orderNew.productsInput = new List<ProductTraceability>();
                orderNew.productsOutput = new List<ProductTraceability>();

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

            await _orderService.updateOrder(order.id, order);

            return product;
        }

        private ProductTraceability CreateProduct(InputData inputData, string ProductName){
            ProductTraceability product = new ProductTraceability();
            product.productId = inputData.productId;
            product.product = ProductName;
            product.batch = inputData.batch;
            product.quantity = inputData.quantity.Value;
            product.date = DateTime.Now.Ticks;
            product.username = inputData.username;
            product.code = inputData.code;
            product.productType = inputData.productType;
            return product;
        }

        private async Task<string> GetProductionOrderApi(int orderId){
            HttpClient client = new HttpClient();
            string OrderRecipe = string.Empty;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var builder = new UriBuilder(_configuration["productionOrdersServiceEndpoint"] + "/api/productionorders/" + orderId);
            Console.WriteLine(builder.ToString());
            string url = builder.ToString();
            var result = await client.GetAsync(url);            
            if (result.StatusCode == HttpStatusCode.OK){
                OrderRecipe = (await client.GetStringAsync(url));
                Console.WriteLine("Retornou bunitu");
                return OrderRecipe;
            }
            Console.WriteLine("Retornando nulo");
            return null;
        }

        private (bool, string, string) CheckProductInRecipe(int ProductId, string OrderRecipe){
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("MyLog");
            Console.WriteLine(OrderRecipe);
            Console.WriteLine("");
            Console.WriteLine("");
            JObject recipeJson = JObject.Parse(OrderRecipe);
            string ProductName = string.Empty;
            string ProductionOrderName = string.Empty;

            var phases = (from r in recipeJson["recipe"]["phases"] select r);

            var phaseProducts = phases.Select(x => x.SelectToken("phaseProducts"));

            foreach (var phaseProduct in phaseProducts)
            {
                var products = phaseProduct.Select(x => x.SelectToken("product"));

                var product = products.FirstOrDefault(x => (int)x.SelectToken("productId") == ProductId);

                if (product != null || ProductId == 70)
                {
                    if(ProductId == 70)
                    {
                        ProductName = "Cobre Fosforoso";
                    }
                    else
                    {
                        ProductName = (string)product.SelectToken("productName");
                    }
                    
                    ProductionOrderName = (string)recipeJson["productionOrderNumber"];
                    return (true, ProductName, ProductionOrderName);
                }

            }


            return (false, null, null);
        }

        private (bool, string, string) CheckProductOutRecipe(int ProductId, string OrderRecipe)
        {
            JObject recipeJson = JObject.Parse(OrderRecipe);
            string ProductName = string.Empty;
            string ProductionOrderName = string.Empty;

            var product = recipeJson.SelectToken("recipe.recipeProduct.product");

            if (ProductId == (int)product.SelectToken("productId"))
            {
                ProductName = (string)product.SelectToken("productName");
                ProductionOrderName = (string)recipeJson.SelectToken("productionOrderNumber");
                return (true, ProductName, ProductionOrderName);
            }
            return (false, null, null);
        }

        public bool ValidateProductIdInRecipe(int productId, int productionOrderId, typeEnum type)
        {
            Console.WriteLine("MyLog2");
            Console.WriteLine(GetProductionOrderApi(productionOrderId).Result);
            Console.WriteLine("MyLog3");
            Console.WriteLine(GetProductionOrderApi(productionOrderId).Result.ToString());
            string recipe = GetProductionOrderApi(productionOrderId).Result;
            bool checkRecipe = false;
            string ProductName = string.Empty;
            string ProductionOrderName = string.Empty;

            // Verifica tipo de processo input ou output
            switch (type)
            {
                case typeEnum.Input:
                    (checkRecipe, ProductName, ProductionOrderName) = CheckProductInRecipe(productId, recipe);
                    break;

                case typeEnum.Output:
                    (checkRecipe, ProductName, ProductionOrderName) = CheckProductOutRecipe(productId, recipe);
                    break;

            }

            return checkRecipe;

        }

    }
}