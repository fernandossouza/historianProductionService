using System.Collections.Generic;
using System.Threading.Tasks;
using historianproductionservice.Model;

namespace historianproductionservice.Service.Interface
{
    public interface IProductionOrderService
    {
        Task<List<ProductionOrder>> getProductionOrders(int startat, int quantity,
        string fieldFilter, string fieldValue, string orderField, string order);
        Task<ProductionOrder> getProductionOrder(long productionOrderId);
    }
}