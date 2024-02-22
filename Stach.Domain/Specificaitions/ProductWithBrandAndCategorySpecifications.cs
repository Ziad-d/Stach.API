using Stach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stach.Domain.Specificaitions
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
    {
        // Constructor for creating an object that will be used to get all products
        public ProductWithBrandAndCategorySpecifications(string? sort)
            : base()
        {
            AllIncludes();

            if (!string.IsNullOrEmpty(sort))
            {
                switch(sort)
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
