using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stach.API.DTOs;
using Stach.API.Errors;
using Stach.Domain.Models.Order_Aggregate;
using Stach.Domain.Services;

namespace Stach.API.Controllers
{
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost] // POST: /api/Orders
        public async Task<ActionResult<Order>> CreateOrder(OrderDTO orderDTO)
        {
            var address = _mapper.Map<AddressDTO, Address>(orderDTO.ShippingAddress);

            var order = await _orderService.CreateOrderAsync(orderDTO.BuyerEmail, orderDTO.BasketId, orderDTO.DeliveryMethodId, address);

            if (order == null)
                return BadRequest(new ApiResponse(400));

            return Ok(order);
        }

        [HttpGet] // GET: /api/Orders?email=ziad.saleh@linkdev.com
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForUser(string email)
        {
            var orders = await _orderService.GetOrdersForUserAsync(email);

            return Ok(orders);
        }

        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")] // GET: /api/Orders/1?email=ziad.saleh@linkdev.com
        public async Task<ActionResult<Order>> GetOrderForUser(int id, string email)
        {
            var order = await _orderService.GetOrderByIdForUserAsync(id, email);

            if (order == null) return NotFound(new ApiResponse(404));

            return Ok(order);
        }
    }
}
