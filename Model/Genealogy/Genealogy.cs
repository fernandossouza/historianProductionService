
using System.Collections.Generic;

namespace historianproductionservice.Model.Genealogy{
    public class Genealogy{   
        public Genealogy(long productionOrderId, string productionOrderNumber, long startDate, long endDate, List<EndRoll> outputRolls){
            this.orderId = productionOrderId;
            this.productionOrderNumber = productionOrderNumber;
            this.startDate = startDate;
            this.outputRolls = outputRolls;
        }

        public Genealogy(){ }
        public long id {get;set;}     
        public long orderId { get; set; } 
        public string productionOrderNumber { get ; set ; }               
        public long startDate { get; set; }        
        public long endDate { get; set; }
        public string recipeCode { get; set; }  
        public string recipeid { get; set; }  
        public List<EndRoll> outputRolls {get;set;}            
    }
}