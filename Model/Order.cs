using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace historianproductionservice.Model
{
    public class Order
    {
        public int id { get; set; }
        public int productionOrderId { get; set; }
        public string order { get; set; }
        public List<ProductTraceability> productsInput { get; set; }
        public List<ProductTraceability> productsOutput { get; set; }
    }
}