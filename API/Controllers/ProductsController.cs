using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandsRepo;
        private readonly IGenericRepository<ProductType> _productTypesRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo,
        IGenericRepository<ProductBrand> productBrandsRepo, IGenericRepository<ProductType> productTypesRepo, IMapper mapper)
        {
            _mapper = mapper;
            _productTypesRepo = productTypesRepo;
            _productBrandsRepo = productBrandsRepo;
            _productsRepo = productsRepo;

        }

    [HttpGet]
    public async Task<ActionResult<List<ProductToReturnDto>>> getProducts()
    {
        var spec = new ProductsWithTypesAndBrandsSpecifications();
        var products = await _productsRepo.ListAsync(spec);
        return Ok( _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<ProductToReturnDto>> getProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecifications(id);
        var product = await _productsRepo.GetEntityWithSpec(spec);
        return _mapper.Map<Product,ProductToReturnDto>(product);
    }


    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> getProductBrands()
    {
        var productBrands = await _productBrandsRepo.GetListAsync();
        return Ok(productBrands);
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> getProductTypes()
    {
        var productTypes = await _productTypesRepo.GetListAsync();
        return Ok(productTypes);
    }
}
}