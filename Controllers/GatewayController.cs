using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using historianproductionservice.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using securityfilter;

namespace historianproductionservice.Controllers {
    [Route ("")]

    public class GatewayController : Controller {
        private IConfiguration _configuration;
        private readonly IProductionOrderService _productionOrderService;

        public GatewayController (IConfiguration configuration,
            IProductionOrderService productionOrderService) {
            _configuration = configuration;
            _productionOrderService = productionOrderService;
        }

        [HttpGet ("gateway/productionorders/")]
        [Produces ("application/json")]
        [SecurityFilter ("historian_production__allow_read")]
        public async Task<IActionResult> GetRecipes ([FromQuery] int startat, [FromQuery] int quantity, [FromQuery] string fieldFilter, [FromQuery] string fieldValue, [FromQuery] string orderField, [FromQuery] string order) {

            var productionOrders = await _productionOrderService.getProductionOrders (startat, quantity, fieldFilter,
                fieldValue, orderField, order);
            return Ok (productionOrders);
        }

        [HttpGet ("gateway/recipes/{id}")]
        [Produces ("application/json")]
        [SecurityFilter ("historian_production__allow_read")]
        public async Task<IActionResult> GetRecipe (int id) {
            var productionOrder = await _productionOrderService.getProductionOrder (id);
            return Ok (productionOrder);
        }

    }
}