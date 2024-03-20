using Microsoft.Extensions.Configuration;
using Stach.Domain;
using Stach.Domain.Models;
using Stach.Domain.Models.Order_Aggregate;
using Stach.Domain.Repositories;
using Stach.Domain.Services;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Stach.Domain.Models.Product;

namespace Stach.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration, IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string BasketId)
        {
            // Get Secret key
            StripeConfiguration.ApiKey = _configuration["StripeKeys:Secretkey"];

            // Get Basket
            var basket = await _basketRepository.GetBasketAsync(BasketId);
            if (basket is null) return null;

            var shippingPrice = 0M; // Decimal
            if (basket.DeliveryMethodId.HasValue)   // Get Shipping Price
            {
                var deliveryMethod = await _unitOfWork.GetRepo<DeliveryMethod>().GetAsync(basket.DeliveryMethodId.Value);
                shippingPrice = deliveryMethod.Cost;
            }

            // Check if the price of the items is the real price
            if (basket.Items.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.GetRepo<Product>().GetAsync(item.Id);
                    if (item.Price != product.Price)
                        item.Price = product.Price;
                }
            }

            var subTotal = basket.Items.Sum(item => item.Quantity * item.Price);

            // Create Payment Intent
            var service = new PaymentIntentService();

            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))   // Create
            {
                var Options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)(subTotal * 100 + shippingPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" }
                };
                paymentIntent = await service.CreateAsync(Options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else   // Update
            {
                var Options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)(subTotal * 100 + shippingPrice * 100),
                };
                paymentIntent = await service.UpdateAsync(basket.PaymentIntentId, Options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }

            await _basketRepository.UpdateBasketAsync(basket);
            return basket;
        }
    }
}
