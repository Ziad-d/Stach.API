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
        public ProductWithBrandAndCategorySpecifications()
            : base()
        {
            AllIncludes();
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
