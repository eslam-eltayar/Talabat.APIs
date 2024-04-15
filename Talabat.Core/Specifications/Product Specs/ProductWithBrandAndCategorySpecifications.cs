using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Product_Specs
{
	public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
	{
		// This Ctor will be used for creating an object, That will be used to -> Get All Products .
		public ProductWithBrandAndCategorySpecifications()
			: base()
		{
			AddIncludes();
		}


		// This Ctor will be used to creating an object, That will be used to -> Get Product with {id}
		public ProductWithBrandAndCategorySpecifications(int id)
			: base(P => P.Id == id)
		{
			AddIncludes();
		}

		private void AddIncludes()
		{
			Includes.Add(P => P.Brand);
			Includes.Add(P => P.Category);
		}

	}
}
