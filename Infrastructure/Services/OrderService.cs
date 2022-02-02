using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<DeliveryMethod> _dmRepo;

        private readonly IGenericRepository<Order> _orderRepo;

        private readonly IBasketRepository _basketRepo;

        private readonly IGenericRepository<Product> _productRepo;
        
        public OrderService(IGenericRepository<Order> orderRepo, IGenericRepository<DeliveryMethod> dmRepo,
         IGenericRepository<Product> productRepo, IBasketRepository basketRepo)
        {
            _productRepo = productRepo;
            _basketRepo = basketRepo;
            _orderRepo = orderRepo;
            _dmRepo = dmRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            // get basket from repo 
            var basket = await _basketRepo.GetBasketAsync(basketId);
            // get items from the product repo 
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await _productRepo.GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }
            // get delivery method from repo
            var deliveryMethod = await _dmRepo.GetByIdAsync(deliveryMethodId);
            // calc subtotla
            var subtotal = items.Sum(item => item.Price*item.Quantity);
            // create order
            var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal);
            // save to db

            // return order
            return order;
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUseAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}