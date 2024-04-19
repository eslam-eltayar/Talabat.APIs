using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.APIs.Controllers
{
	public class ProductsController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productsRepo;
		private readonly IGenericRepository<ProductBrand> _brandsRepo;
		private readonly IGenericRepository<ProductCategory> _categoriesRepo;
		private readonly IMapper _mapper;

		public ProductsController(IGenericRepository<Product> productsRepo,
			IGenericRepository<ProductBrand> brandsRepo,
			IGenericRepository<ProductCategory> categoriesRepo,
			IMapper mapper)
		{
			_productsRepo = productsRepo;
			_brandsRepo = brandsRepo;
			_categoriesRepo = categoriesRepo;
			_mapper = mapper;
		}


		// /api/Products
		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string sort)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(sort);

			var products = await _productsRepo.GetAllWithSpecAsync(spec);

			var mappedProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);


			return Ok(mappedProducts);
		}

		// /api/products/1
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(id);


			var product = await _productsRepo.GetWithSpecAsync(spec);

			var mappedProduct = _mapper.Map<Product, ProductToReturnDto>(product!);

			if (product is null)
				return NotFound(new ApiResponse(404)); // 404

			return Ok(mappedProduct); // 200
		}


		[HttpGet("brands")] // GET: /api/products/brands
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
		{
			var brands = await _brandsRepo.GetAllAsync();

			return Ok(brands);
		}

		[HttpGet("categories")] // GET: /api/products/categories
		public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
		{
			var categories = await _categoriesRepo.GetAllAsync();

			return Ok(categories);
		}
	}
}
