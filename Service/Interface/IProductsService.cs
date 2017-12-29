using System.Threading.Tasks;
using System.Collections.Generic;
using historianproductionservice.Model;

namespace historianproductionservice.Service.Interface
{
    public interface IProductsService
    {
        Task<InputData> addProduct(InputData inputData);

        bool ValidateProductIdInRecipe(int productId,int productionOrderId, typeEnum type);
   
    }
}