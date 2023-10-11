using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodfella.Core.Models
{
	public class Restaurant
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(100)] 
		public string Name { get; set; }

		[Required]
		[MaxLength(50)] 
		public string CuisineType { get; set; }

		[Required]
		[MaxLength(200)] 
		public string Location { get; set; }

		[Range(0.0, 5.0)] // Assuming average ratings are on a scale of 0 to 5
		public double AverageRating { get; set; }

		public List<MenuItem> MenuItems { get; set; }

		[Required]
		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }
	}

}

