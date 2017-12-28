using System.ComponentModel.DataAnnotations;

namespace historianproductionservice.Model
{
    public class InputData
    {
        [Required]
        public typeEnum type{get;set;}
        [Required]
        public int orderId{get;set;}
        [Required]
        public int productId{get;set;}
        [Required]
        public double? quantity{get;set;}
        [Required]
        public string batch{get;set;}
    }
}