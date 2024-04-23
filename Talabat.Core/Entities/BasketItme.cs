using System.Reflection.Metadata.Ecma335;

namespace Talabat.Core.Entities
{
	public class BasketItme
	{
		public int Id { get; set; }
		public string ProductName { get; set; } = null!;
		public string PictureUrl { get; set; } = null!;
		public decimal Price { get; set; }
		public string Category { get; set; } = null!;
		public string Brand { get; set; } = null!;
		public int Quantity { get; set; }


	}
}