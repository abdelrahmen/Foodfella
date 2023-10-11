using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodfella.Core.Models
{
	public class OrderDetail
	{
		public int Id { get; set; }

		[Required]
		public int OrderId { get; set; }

		[Required]
		public int MenuItemId { get; set; }

		[Required]
		[Range(1, 100)]
		public int Quantity { get; set; }

		[Required]
		[Range(0, double.MaxValue)]
		public decimal Price { get; set; } //price at the moment of making order

		[ForeignKey(nameof(MenuItemId))]
		public MenuItem MenuItem { get; set; }
	}
}
