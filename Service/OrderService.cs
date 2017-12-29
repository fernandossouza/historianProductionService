using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using historianproductionservice.Service.Interface;
using historianproductionservice.Model;
using historianproductionservice.Data;

namespace historianproductionservice.Service
{
    public class OrderService : IOrderService
    {
        
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public OrderService(IConfiguration configuration, ApplicationDbContext context)
        {
            _context =   context;
            _configuration = configuration;
        }

        public async Task<Order> getOrderId(int orderId)
        {
            var order = await _context.Orders
                        .Include(x =>x.productsInput)
                        .Include(x =>x.productsOutput)
                        .Where(x=>x.id == orderId)                        
                        .FirstOrDefaultAsync();
            
            return order;
        }

        public async Task<Order> getProductionOrderId(int productionOrderId)
        {
            var order = await _context.Orders
                        .Include(x =>x.productsInput)
                        .Include(x =>x.productsOutput)
                        .Where(x=>x.productionOrderId == productionOrderId)                        
                        .FirstOrDefaultAsync();
            
            return order;
        }

        public async Task<Order> updateOrder(int orderId,Order order)
        {
            var orderDB = await _context.Orders
                        .Where(x=>x.id == orderId) 
                        .AsNoTracking()                       
                        .FirstOrDefaultAsync();;


            if (orderId != orderDB.id && orderDB == null)
            {
                return null;
            }           

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> addOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}