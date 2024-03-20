using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stach.API.DTOs;
using Stach.API.Errors;
using Stach.Domain.Models;
using Stach.Domain.Services;

namespace Stach.API.Controllers
{
    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public PaymentsController(IPaymentService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }

        public async Task<ActionResult<CustomerBasketDTO>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var customerBasket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

            if (customerBasket == null) return BadRequest(new ApiResponse(400, "there is a problem with your basket!"));

            var mappedBasket = _mapper.Map<CustomerBasket, CustomerBasketDTO>(customerBasket);

            return Ok(mappedBasket);
        }
    }
}
