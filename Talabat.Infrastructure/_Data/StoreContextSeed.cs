using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Infrastructure.Data
{
	public static class StoreContextSeed
	{
		public async static Task SeedAsync(StoreContext _dbContext)
		{
			// Product Brand Seeding
			if (! _dbContext.ProductBrands.Any())
			{
				var brandsData = File.ReadAllText("../Talabat.Infrastructure/_Data/DataSeed/brands.json");

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
			if (! _dbContext.ProductCategories.Any())
			{
				var categoriesData = File.ReadAllText("../Talabat.Infrastructure/_Data/DataSeed/categories.json");

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
			if (! _dbContext.Products.Any())
			{
				var productsData = File.ReadAllText("../Talabat.Infrastructure/_Data/DataSeed/products.json");

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


			// DeliveryMethods Seeding
            if (!_dbContext.DeliveryMethods.Any())
            {
                var delviryMethodsData = File.ReadAllText("../Talabat.Infrastructure/_Data/DataSeed/delivery.json");

                var delviryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(delviryMethodsData);

                if (delviryMethods?.Count > 0)
                {
                    foreach (var deliveryMethod in delviryMethods)
                    {
                        _dbContext.Set<DeliveryMethod>().Add(deliveryMethod);
                    }

                    await _dbContext.SaveChangesAsync();
                }
            }
        }
	}
}
