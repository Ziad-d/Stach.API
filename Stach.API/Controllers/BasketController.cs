using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stach.API.Errors;
using Stach.Domain.Models;
using Stach.Domain.Repositories;

namespace Stach.API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet] // GET: /api/Basket?id=
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            // if basket requested is expired then create new basket
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost] // POST: /api/Basket?id=
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var createdOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(basket);
            if (createdOrUpdatedBasket is null) return BadRequest(new ApiResponse(400));

            return Ok(createdOrUpdatedBasket);
        }

        [HttpDelete] // DELETE: /api/Basket?id=
        public async Task DeleteBasket(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
