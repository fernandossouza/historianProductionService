using System.ComponentModel.DataAnnotations;
namespace historianproductionservice.Model
{
    public class Product
    {
        public int id{get;set;}
        public int productId{get;set;}
        public string product{get;set;}
        public double quantity{get;set;}
        public string batch{get;set;}
        public long date{get;set;}
    }
}