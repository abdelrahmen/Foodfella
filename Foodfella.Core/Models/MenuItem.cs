using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foodfella.Core.Models
{

	public class MenuItem
	{
		public int Id { get; set; }

		[Required]
		public int RestaurantId { get; set; }

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
		[MaxLength(50)]
		public string Status { get; set; } //available or not available

		[Required]
		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }

		[ForeignKey(nameof(RestaurantId))]
		public Restaurant Restaurant { get; set; }

		public MenuItem()
		{
			this.CreatedAt = DateTime.Now;
			this.Status = "available";
		}
	}

}
