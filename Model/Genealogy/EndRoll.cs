using System.Collections.Generic;

namespace historianproductionservice.Model.Genealogy{
    public class EndRoll{
        public EndRoll()
        {
            
        }        
        public long id {get;set;}
        public long productionOrderId {get;set;}   
        public string quantity { get; set; }
        public List<Aco> inputRolls {get;set;}
        public List<Liga> ligas {get;set;}
        public List<Tool> tools {get;set;}
        public long? startDate {get;set;}
        public long endDate {get;set;}            
    }
}