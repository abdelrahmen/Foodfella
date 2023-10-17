using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodfella.Core.DTOs
{
	public class MenuUpdateDTO
	{
		[MaxLength(100)]
		public string Name { get; set; }

		[MaxLength(500)]
		public string Description { get; set; }

		[Range(0, double.MaxValue)]
		public decimal Price { get; set; }

		[MaxLength(50)]
		public string Category { get; set; }

		public int RestaurantId { get; set; }

		[MaxLength(50)]
		public string Status { get; set; } // available or not available
	}
}
