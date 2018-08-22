namespace historianproductionservice.Model.Genealogy
{
    public class Elemento{

        public Elemento(string product, string quantity, string batch, long date){            
            this.product = product;
            this.quantity = quantity;
            this.batch = batch;
            this.date = date;
        }
        public Elemento(){}
        public long id {get;set;}
        public string product{get;set;}
        public string quantity{get;set;}
        public string batch{get;set;}
        public long date{get;set;}
    }
}