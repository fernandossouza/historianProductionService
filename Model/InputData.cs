using System.ComponentModel.DataAnnotations;

namespace historianproductionservice.Model
{
    public class InputData
    {
        [Required]
        public typeEnum type{get;set;}
        [Required]
        public string order{get;set;}
        [Required]
        public string product{get;set;}
        [Required]
        public double? quantity{get;set;}
        [Required]
        public string batch{get;set;}
    }
}