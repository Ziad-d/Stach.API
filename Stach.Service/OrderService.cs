using Stach.Domain;
using Stach.Domain.Models;
using Stach.Domain.Models.Order_Aggregate;
using Stach.Domain.Repositories;
using Stach.Domain.Services;
using Stach.Domain.Specificaitions.Order_Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stach.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork)
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            // 1. Getting Basket from Basket Repo
            var basket = await _basketRepo.GetBasketAsync(basketId);

            // 2. Getting Selected Items at Basket from Product Repo
            var orderItems = new List<OrderItem>();

            if(basket?.Items?.Count > 0)
            {
                // 2.1. Getting product repository
                var productRepository = _unitOfWork.GetRepo<Product>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepository.GetAsync(item.Id);
                    var productItemOrdered = new ProductItemOrdered(item.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);
                    orderItems.Add(orderItem);
                }
            }

            // 3. Calculating Subtotal
            var subTotal = orderItems.Sum(orderItem => orderItem.Price * orderItem.Quantity);

            // 4. Getting DelivaryMethod from DelivaryMethod Repo
            var deliveryMethod = await _unitOfWork.GetRepo<DeliveryMethod>().GetAsync(deliveryMethodId);

            // 5. Creating Order
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subTotal);

            await _unitOfWork.GetRepo<Order>().AddAsync(order);

            // 6. Saving to Database
            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                return null;

            return order;
        }

        public Task<Order?> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var orderRepo = _unitOfWork.GetRepo<Order>();

            var orderSpec = new OrderSpecifications(orderId, buyerEmail);

            var order = orderRepo.GetWithSpecAsync(orderSpec);

            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var orderRepo = _unitOfWork.GetRepo<Order>();

            var spec = new OrderSpecifications(buyerEmail);

            var orders = await orderRepo.GetAllWithSpecAsync(spec);

            return orders;
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            var deliveryMethodsRepo = _unitOfWork.GetRepo<DeliveryMethod>();

            var deliveryMethods = deliveryMethodsRepo.GetAllAsync();

            return deliveryMethods;
        }
    }
}
