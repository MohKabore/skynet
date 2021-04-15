using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitWork _unitOfWork;
        public OrderService(IBasketRepository basketRepo, IUnitWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _basketRepo = basketRepo;
       
        }

        public async Task<Order> createOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            // get basket friom repo
            var basket = await _basketRepo.GetBasketAsync(basketId);

            // get item from the porduct repo
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, item.Quantity, productItem.Price);
                items.Add(orderItem);
            }

            // get deliveryMethod from repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            // calc subtotal
            var subtotal = items.Sum(a => a.Price * a.Quantity);

            //create the order
            var order = new Order(items, subtotal, buyerEmail, shippingAddress, deliveryMethod);
            _unitOfWork.Repository<Order>().Add(order);

            //TODO:save  to db
            var result =await _unitOfWork.Complete();
            if(result<=0) return null ;

            await _basketRepo.DeleteBasketAsync(basketId);


            // return the order
            return order;

        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
          return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderbyIdAsync(int id, string buyerEmail)
        {
           var spec = new OrdersWithItemsAndOrderingSpecification(id,buyerEmail);
          return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
          var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);
          return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}