using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using historianproductionservice.Model;
using historianproductionservice.Service.Interface;

namespace historianproductionservice.Service
{
    public class ProductionOrderService : IProductionOrderService
    {
        private IConfiguration _configuration;
        private HttpClient client = new HttpClient();

        public ProductionOrderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<ProductionOrder> getProductionOrder(int productionOrderId)
        {
            try
            {
                ProductionOrder returns = null;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var builder = new UriBuilder(_configuration["productionOrdersServiceEndpoint"] + "/api/productionorders/" + productionOrderId);
                string url = builder.ToString();
                var result = await client.GetAsync(url);
                switch (result.StatusCode)
                {
                    case HttpStatusCode.OK:
                        returns = JsonConvert.DeserializeObject<ProductionOrder>(await client.GetStringAsync(url));
                        return returns;
                    case HttpStatusCode.NotFound:
                        return returns;
                    case HttpStatusCode.InternalServerError:
                        return returns;
                }
                return returns;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<List<ProductionOrder>> getProductionOrders(int startat, int quantity, string fieldFilter, string fieldValue, string orderField, string order)
        {
            try
            {
                List<ProductionOrder> returns = null;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var builder = new UriBuilder(_configuration["productionOrdersServiceEndpoint"] + "/api/productionorders");
                var query = HttpUtility.ParseQueryString(builder.Query);
                if (startat != 0)
                    query["startat"] = startat.ToString();
                if (quantity != 0)
                    query["quantity"] = quantity.ToString();
                if (!string.IsNullOrEmpty(fieldFilter))
                    query["fieldFilter"] = fieldFilter;
                if (!string.IsNullOrEmpty(fieldValue))
                    query["fieldValue"] = fieldValue;
                if (!string.IsNullOrEmpty(orderField))
                    query["orderField"] = orderField;
                if (!string.IsNullOrEmpty(order))
                    query["order"] = order;
                builder.Query = query.ToString();
                string url = builder.ToString();
                var result = await client.GetAsync(url);
                switch (result.StatusCode)
                {
                    case HttpStatusCode.OK:
                        string returnJson = (await client.GetStringAsync(url));
                        var returnTagString = JObject.Parse(returnJson)["values"];
                        string recipes = returnTagString.ToString();
                        returns = JsonConvert.DeserializeObject<List<ProductionOrder>>(recipes);
                        return returns;
                    case HttpStatusCode.NotFound:
                        return returns;
                    case HttpStatusCode.InternalServerError:
                        return returns;
                }
                return returns;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}