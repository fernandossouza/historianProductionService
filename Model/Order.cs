using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace historianproductionservice.Model
{
    public class Order
    {
        public int id{get;set;}
        public string order{get;set;}
        public IList<Product> productsInput{get;set;}
        public IList<Product> productsOutput{get;set;}
    }
}