using System.Collections.Generic;

namespace historianproductionservice.Model.Genealogy{
    public class Tool{
        public Tool(string toolId, string typeName, string serialNumber, string vidaUtil){
            this.toolId = toolId;
            this.typeName = typeName;
            this.serialNumber = serialNumber;
            this.vidaUtil = vidaUtil;
        }

        public Tool(){}
        public long id {get;set;}
        public string toolId {get;set;}
        public string typeName{get;set;}
        public string serialNumber{get;set;}
        public string vidaUtil{get;set;}
        public string group{get; set;}
    }
}    