using System.ComponentModel.DataAnnotations;

namespace historianproductionservice.Model
{
    public class InputData
    {
        [Required]
        public typeEnum type{get;set;}
        [Required]
        public int productionOrderId{get;set;}
        [Required]
        [ValidadeProductInRecipe]
        public int productId{get;set;}
        [Required]
        public double? quantity{get;set;}
        [Required]
        public string batch{get;set;}
    }
}