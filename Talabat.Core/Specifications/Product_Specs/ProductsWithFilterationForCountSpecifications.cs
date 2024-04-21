using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Product_Specs
{
	public class ProductsWithFilterationForCountSpecifications : BaseSpecifications<Product>
	{
		public ProductsWithFilterationForCountSpecifications(ProductSpecParams productSpecParams)
			: base(P =>

			        (!productSpecParams.BrandId.HasValue    || P.BrandId == productSpecParams.BrandId) &&
					(!productSpecParams.CategoryId.HasValue || P.CategoryId == productSpecParams.CategoryId)

			)
		{



		}
	}
}
