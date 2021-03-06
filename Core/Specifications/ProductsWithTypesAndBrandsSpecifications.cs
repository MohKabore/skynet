using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecifications : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecifications()
        {
            AddInclude(x=>x.ProductBrand);
            AddInclude(x=>x.ProductType);
        }

        public ProductsWithTypesAndBrandsSpecifications(int id) : base(x=>x.Id ==id)
        {
            AddInclude(x=>x.ProductBrand);
            AddInclude(x=>x.ProductType);
        }
    }
}