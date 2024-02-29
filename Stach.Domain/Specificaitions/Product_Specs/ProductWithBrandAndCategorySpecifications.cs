using Stach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stach.Domain.Specificaitions.Product_Specs
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
    {
        // Constructor for creating an object that will be used to get all products
        public ProductWithBrandAndCategorySpecifications(ProductSpecParams specParams)
            : base(P => (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search))
                  &&    (!specParams.BrandId.HasValue || P.BrandId == specParams.BrandId.Value)
                  &&    (!specParams.CategoryId.HasValue || P.CategoryId == specParams.CategoryId.Value))
        {
            AllIncludes();

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            else
                AddOrderBy(P => P.Name);

            ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);
        }

        // Constructor for creating an object that will be used to get a specific product
        public ProductWithBrandAndCategorySpecifications(int id)
            : base(p => p.Id == id)
        {
            AllIncludes();
        }

        private void AllIncludes()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
    }
}
