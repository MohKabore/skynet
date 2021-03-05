using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

       

        public async Task<Product> getProductByIdAsync(int id)
        {
            var product = await _context.Products
                                .Include(p=>p.ProductBrand)
                                .Include(p=>p.ProductType)
                                .FirstOrDefaultAsync(p=>p.Id == id);
            return product;
        }

        public async Task<IReadOnlyList<Product>> getProductsAsync()
        {
            var products = await _context.Products
                            .Include(p=>p.ProductBrand)
                            .Include(p=>p.ProductType)
                            .ToListAsync();
            return products;
        }

        public async Task<IReadOnlyList<ProductType>> getProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
         public async Task<IReadOnlyList<ProductBrand>> getProductBrandsAsync()
        {
              return await _context.ProductBrands.ToListAsync();
        }
    }
}