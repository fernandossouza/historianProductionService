using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace historianproductionservice.Model
{
    public class ProductTraceability
    {
        public int id { get; set; }
        public int productId { get; set; }
        public string product { get; set; }
        public double quantity { get; set; }
        public string batch { get; set; }
        public long date { get; set; }
        public string unity{get;set;}
        public string username{get;set;}
        public string code{get;set;}
        public string productType{get; set;}  
        [NotMapped]
        public long endDate{get; set;}  
    }
}