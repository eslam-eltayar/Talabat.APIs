using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
	public class Product : BaseEntity
	{
		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
		public string PictureUrl { get; set; } = null!;
		public decimal Price { get; set; }


		public int BrandId { get; set; }  // Foreign Key Column => Product Brand
		public ProductBrand Brand { get; set; } = null!;  // Navigational Property [ONE] 


		
		public int CategoryId { get; set; }  // Foreign Key Column =>ProductCategory
		public ProductCategory Category { get; set; } = null!;  // Navigational Property [ONE] 
	}
}
