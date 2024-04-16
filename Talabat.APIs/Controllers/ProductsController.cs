using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.APIs.Controllers
{
	public class ProductsController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productsRepo;
		private readonly IMapper _mapper;

		public ProductsController(IGenericRepository<Product> productsRepo, IMapper mapper)
		{
			_productsRepo = productsRepo;
			_mapper = mapper;
		}


		// /api/Products
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			var spec = new ProductWithBrandAndCategorySpecifications();

			var products = await _productsRepo.GetAllWithSpecAsync(spec);

			var mappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products);


			return Ok(mappedProducts);
		}

		// /api/products/1
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(id);


			var product = await _productsRepo.GetWithSpecAsync(spec);

			var mappedProduct = _mapper.Map<Product, ProductToReturnDto>(product!);

			if (product is null)
				return NotFound(); // 404

			return Ok(mappedProduct); // 200
		}


	}
}
