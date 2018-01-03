using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace historianproductionservice.Model
{

    public enum PhaseProductType
    {
        scrap,
        finished,
        semi_finished,
    }
    public class PhaseProduct
    {

        public string value { get; set; }
        public string measurementUnit { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public PhaseProductType phaseProductType { get; set; }
        public Product product { get; set; }
    }

}