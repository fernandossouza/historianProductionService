using System.Threading.Tasks;
using System.Collections.Generic;
using historianproductionservice.Model;

namespace historianproductionservice.Service.Interface
{
    public interface IOrderService
    {
        Task<Order> getOrderId(int orderId);
        Task<Order> getProductionOrderId(int productionOrderId);
        Task<Order> updateOrder(int orderId,Order order);
        Task<Order> addOrder(Order order);
    }
}