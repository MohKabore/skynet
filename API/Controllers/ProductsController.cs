using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;
        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> getProducts()
        {
            var products = await _repo.getProductsAsync();
            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<Product>>> getProduct(int id)
        {
            var product = await _repo.getProductByIdAsync(id);
            return Ok(product);
        }


         [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> getProductBrands()
        {
            var productBrands = await _repo.getProductBrandsAsync();
            return Ok(productBrands);
        }

         [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> getProductTypes()
        {
            var productTypes = await _repo.getProductTypesAsync();
            return Ok(productTypes);
        }
    }
}