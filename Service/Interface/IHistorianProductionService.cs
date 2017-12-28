using System.Threading.Tasks;
using System.Collections.Generic;
using historianproductionservice.Model;

namespace historianproductionservice.Service.Interface
{
    public interface IHistorianProductionService
    {
        Task<Order> getOrder(int orderId);

        Task<InputData> addProduct(InputData inputData);


         
    }
}