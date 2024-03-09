using Stach.Domain.Models;
using Stach.Domain.Specificaitions.Product_Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stach.Domain.Services
{
    public interface IProductService
    {
        Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams specParams);

        Task<Product?> GetProductAsync(int productId);

        Task<int> GetCountAsync(ProductSpecParams specParams);

        Task<IReadOnlyList<ProductBrand>> GetBrandsAsync();

        Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync();
    }
}
