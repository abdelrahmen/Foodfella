using Foodfella.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodfella.Core.DTOs
{
	public class CartItemDTO
	{
		public int Id { get; set; }
		public int MenuItemId { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		public static object FromCartItem(CartItem c)
		=> new CartItemDTO
		{
			CreatedAt = c.CreatedAt,
			Id = c.Id,
			MenuItemId = c.MenuItemId,
			Quantity = c.Quantity,
			Price = c.Price,
			UpdatedAt = c.UpdatedAt,
		};
	}

}
