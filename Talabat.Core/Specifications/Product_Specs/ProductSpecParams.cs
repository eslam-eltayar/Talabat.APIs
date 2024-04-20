using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications.Product_Specs
{
	public class ProductSpecParams
	{
		private const int maxPageSize = 10;
		private int pageSize = 5;
		public int PageSize
		{
			get => pageSize;
			set => pageSize = value > 10 ? 10 : value;

		}

        public int PageIndex { get; set; }
        public string? Sort { get; set; }
		public int? BrandId { get; set; }
		public int? CategoryId { get; set; }
	}
}
