using System.Collections.Generic;

namespace historianproductionservice.Model.Genealogy{
    public class Liga{

        public Liga(long orderId, string orderNumber, long startDate, long endDate, string code, string quantity, string batch, List<ProductTraceability> productsInput){
            this.orderId =orderId;
            this.orderNumber = orderNumber;
            this.startDate = startDate;
            this.endDate = endDate;
            this.code =code;
            this.quantity = quantity;
            this.batch = batch;
            this.productsInput = new List<Elemento>();
            foreach(ProductTraceability p in productsInput)
                this.productsInput.Add(new Elemento(p.product, p.quantity.ToString(), p.batch, p.date));
        }
        public Liga(){}
        public long id {get;set;}
        public long orderId {get;set;}
        public string orderNumber {get;set;}
        public long startDate {get;set;}                        
        public long endDate {get;set;}      
        public string code {get; set;}
        public string quantity {get; set;}
        public string batch {get; set;}                  
        public List<Elemento> productsInput {get;set;}
    }
}