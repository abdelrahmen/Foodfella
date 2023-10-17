using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodfella.Core.DTOs
{
	public class MenuCreateDTO
	{
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[MaxLength(500)]
		public string Description { get; set; }

		[Required]
		[Range(0, double.MaxValue)]
		public decimal Price { get; set; }

		[Required]
		[MaxLength(50)]
		public string Category { get; set; }

		[Required]
		public int RestaurantId {  get; set; }

		[Required]
		[MaxLength(50)]
		public string Status { get; set; } // available or not available
	}
}
