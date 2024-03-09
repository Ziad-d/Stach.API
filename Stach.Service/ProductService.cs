using Stach.Domain;
using Stach.Domain.Models;
using Stach.Domain.Services;
using Stach.Domain.Specificaitions.Product_Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stach.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
        {
            var brands = _unitOfWork.GetRepo<ProductBrand>().GetAllAsync();
            return brands;
        }

        public Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync()
        {
            var categories = _unitOfWork.GetRepo<ProductCategory>().GetAllAsync();
            return categories;
        }

        public async Task<int> GetCountAsync(ProductSpecParams specParams)
        {
            var countSpec = new ProductWithFiltrationForCountSpecification(specParams);
            var count = await _unitOfWork.GetRepo<Product>().GetCountAsync(countSpec);
            return count;
        }

        public async Task<Product?> GetProductAsync(int productId)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(productId);
            var product = await _unitOfWork.GetRepo<Product>().GetWithSpecAsync(spec);
            return product;
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams specParams)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(specParams);
            var products = await _unitOfWork.GetRepo<Product>().GetAllWithSpecAsync(spec);
            return products;
        }
    }
}
