using Stach.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stach.Domain.Specificaitions.Product_Specs
{
    public class ProductWithFiltrationForCountSpecification : BaseSpecifications<Product>
    {
        public ProductWithFiltrationForCountSpecification(ProductSpecParams specParams)
            : base(P => (!specParams.BrandId.HasValue || P.BrandId == specParams.BrandId.Value)
                  && (!specParams.CategoryId.HasValue || P.CategoryId == specParams.CategoryId.Value))
        {

        }
        
    }
}
