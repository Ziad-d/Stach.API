using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stach.Domain.Models;
using Stach.Domain.Repositories;

namespace Stach.API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;

        public ProductsController(IGenericRepository<Product> productsRepo)
        {
            _productsRepo = productsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productsRepo.GetAllAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productsRepo.GetAsync(id);

            if (product == null) 
                return NotFound();

            return Ok(product);
        }
    }
}
