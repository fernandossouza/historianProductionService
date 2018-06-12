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

        public ProductHistorianController (IProductsService productService) {
            _productService = productService;
        }

        [HttpPost]
        [SecurityFilter ("historian_productio__allow_update")]
        public async Task<IActionResult> Post ([FromBody] InputData inputData) {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Entrou \n Model");
            Console.WriteLine(inputData);
            Console.WriteLine();
            Console.WriteLine();
            try {
                if (ModelState.IsValid) {
                    inputData = await _productService.addProduct (inputData);

                    if (inputData == null)
                        return StatusCode (500, "erro in api");

                    return Created ($"api/Product/", inputData);
                }
                return BadRequest (ModelState);
            } catch (Exception ex) {
                return StatusCode (500, ex.ToString ());
            }
        }

    }
}