using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace historianproductionservice.Model
{
    public class InputData
    {
        [Required]
        public typeEnum type{get;set;}
        [Required]
        public int productionOrderId{get;set;}
        [Required]
        //[ValidadeProductInRecipe]
        public int productId{get;set;}
        [Required]
        public double? quantity{get;set;}
        [Required]        
        public string batch{get;set;}        
        public string unity{get;set;}
        public string username{get;set;}
        public string code{get; set;}   
        public string productType{get; set;}               

        public string ToString(){
            return "Type : "+ type + "\nproductionOrderId : " + productionOrderId + "\nproductId : " + productId +"\nquantity : " + quantity + "\nbatch : " + batch + "\nunity : " + unity + "\ncode : " + code + "\nproductType : "+ productType;        
        } 
    }
}