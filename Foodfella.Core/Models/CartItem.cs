using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foodfella.Core.Models
{

	public class CartItem
	{
		public int Id { get; set; }

		[Required]
		public string UserId { get; set; }

		[Required]
		public int MenuItemId { get; set; }

		[Required]
		[Range(1, 100)]
		public int Quantity { get; set; }

		[Required]
		[Range(0, double.MaxValue)]
		public decimal Price { get; set; }

		[Required]
		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }

		[ForeignKey(nameof(MenuItemId))]
		public MenuItem MenuItem { get; set; }
		
		[ForeignKey(nameof(UserId))]
		public ApplicationUser User { get; set; }

		public CartItem()
		{
			this.CreatedAt = DateTime.Now;
		}
	}

}
