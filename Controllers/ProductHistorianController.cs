using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using historianproductionservice.Model;
using historianproductionservice.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using securityfilter;

namespace historianproductionservice.Controllers {
    [Route ("api/[controller]")]
    public class ProductHistorianController : Controller {
        private readonly IProductsService _productService;
        private readonly IGenealogyService _genealogyService;
        public ProductHistorianController (IProductsService productService, IGenealogyService genealogyService) {
            _productService = productService;
            _genealogyService = genealogyService; 
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] long? startDate, [FromQuery] long? endDate, [FromQuery] string op, [FromQuery] string cod) {  
            if(op != null)
                return Ok(await _genealogyService.getByOp(op));
            else if(startDate != null && endDate != null)
                if(cod != null)
                    return Ok(await _genealogyService.getByCode(startDate, endDate, cod));
                else
                    return Ok(await _genealogyService.getByDate(startDate, endDate));            
            else
                return BadRequest("Parametros inválidos");             
        }

        [HttpPost]        
        public async Task<IActionResult> Post([FromBody] InputData inputData) {                    
            try {
                string saveG = "";
                Console.WriteLine("Entrou");
                if (ModelState.IsValid) {                    
                    if(inputData.productType == "saida"){
                        saveG = await _genealogyService.addEndRoll(inputData);
                        if(saveG != "true")    
                            return BadRequest(saveG);                    
                    }
                    inputData = await _productService.addProduct(inputData);
                    if (inputData == null)
                        return StatusCode (500, "erro in api");
                    return Created ($"api/Product/", inputData);
                }
                return BadRequest("ModelState");
            } catch (Exception ex) {
                return StatusCode (500, ex.ToString ());
            }
        }

    }
}