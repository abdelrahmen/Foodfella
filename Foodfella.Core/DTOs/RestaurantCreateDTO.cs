using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodfella.Core.DTOs
{
	public class RestaurantCreateDTO
	{
		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[Required]
		[MaxLength(50)]
		public string CuisineType { get; set; }

		[Required]
		[MaxLength(200)]
		public string Location { get; set; }
	}
}
