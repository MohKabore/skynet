using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.OrderAggregate;

namespace Core.Interfaces
{
    public interface IOrderService
    {
         Task<Order> createOrderAsync(string buyerEmail,int deliveryMethodId, string basketId,Address shippingAddress);
         Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
         Task<Order> GetOrderbyIdAsync(int id, string buyerEmail);
          Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}