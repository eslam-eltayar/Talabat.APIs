using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.APIs.Controllers
{
	public class ProductsController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productsRepo;

		public ProductsController(IGenericRepository<Product> productsRepo)
		{
			_productsRepo = productsRepo;
		}


		// /api/Products
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			var spec = new ProductWithBrandAndCategorySpecifications();

			var products = await _productsRepo.GetAllWithSpecAsync(spec);

			return Ok(products);
		}

		// /api/products/1
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(id);


			var product = await _productsRepo.GetWithSpecAsync(spec);

			if (product is null)
				return NotFound(); // 404

			return Ok(product); // 200
		}


	}
}
