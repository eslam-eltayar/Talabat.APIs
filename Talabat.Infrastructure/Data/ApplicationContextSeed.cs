using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Infrastructure.Data
{
	public static class ApplicationContextSeed
	{
		public async static Task SeedAsync(ApplicationDbContext _dbContext)
		{
			// Product Brand Seeding
			if ( _dbContext.ProductBrands.Count() == 0)
			{
				var brandsData = File.ReadAllText("../Talabat.Infrastructure/Data/DataSeed/brands.json");

				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

				if (brands?.Count > 0)
				{
					foreach (var brand in brands)
					{
						_dbContext.Set<ProductBrand>().Add(brand);
					}

					await _dbContext.SaveChangesAsync();
				}
			} 
			

			// Product Category Seeding
			if ( _dbContext.ProductCategories.Count() == 0)
			{
				var categoriesData = File.ReadAllText("../Talabat.Infrastructure/Data/DataSeed/categories.json");

				var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);

				if (categories?.Count > 0)
				{
					foreach (var category in categories)
					{
						_dbContext.Set<ProductCategory>().Add(category);
					}

					await _dbContext.SaveChangesAsync();
				}
			} 
			

			// Product Seeding
			if ( _dbContext.Products.Count() == 0)
			{
				var productsData = File.ReadAllText("../Talabat.Infrastructure/Data/DataSeed/products.json");

				var products = JsonSerializer.Deserialize<List<Product>>(productsData);

				if (products?.Count > 0)
				{
					foreach (var product in products)
					{
						_dbContext.Set<Product>().Add(product);
					}

					await _dbContext.SaveChangesAsync();
				}
			} 
		}
	}
}
