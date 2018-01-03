using System.Collections.Generic;

namespace historianproductionservice.Model
{
    public class Phase
    {
        public string phaseName { get; set; }
        public string phaseCode { get; set; }
        public ICollection<PhaseProduct> phaseProducts { get; set; }
    }
}