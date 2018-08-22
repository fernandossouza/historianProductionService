using System.ComponentModel.DataAnnotations.Schema;
namespace historianproductionservice.Model.Genealogy
{
    public class Aco{
        public Aco(string quantity, string batch, long startDate, long endDate){                        
            this.quantity = quantity;
            this.batch = batch;
            this.startDate = startDate;
            this.endDate = endDate;
        }
        public Aco(){}
        public long id {get; set;}                
        public string code {get; set;}
        public string quantity {get; set;}
        public string batch {get; set;}
        public long startDate {get; set;}        
        public long endDate {get; set;}        
    }
}