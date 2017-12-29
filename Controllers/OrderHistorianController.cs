using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using historianproductionservice.Service.Interface;
using historianproductionservice.Model;

namespace historianproductionservice.Controllers
{
    [Route("api/[controller]")]
    public class OrderHistorianController: Controller
    {
         private readonly IOrderService _orderService;

          public OrderHistorianController (IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try{
                var tool = await _orderService.getProductionOrderId(id);

                if(tool == null)
                    return NotFound();

                return Ok(tool);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}