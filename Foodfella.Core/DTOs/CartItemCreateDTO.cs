using System.ComponentModel.DataAnnotations;

namespace Foodfella.Core.DTOs
{
	public class CartItemCreateDTO
	{
		[Required]
		public int MenuItemId { get; set; }

		[Required]
		[Range(1, 100)]
		public int Quantity { get; set; }

		[Required]
		[Range(0, double.MaxValue)]
		public decimal Price { get; set; }
	}

}
