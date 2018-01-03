using System.Collections.Generic;

namespace historianproductionservice.Model
{
    public class Product
    {

        public int productId { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public string productCode { get; set; }
        public string productGTIN { get; set; }
        public ICollection<AdditionalInformation> additionalInformation { get; set; }
    }
}