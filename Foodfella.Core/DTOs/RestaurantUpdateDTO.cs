using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodfella.Core.DTOs
{
	public class RestaurantUpdateDTO
	{
		public string Name { get; set; }
		public string CuisineType { get; set; }
		public string Location { get; set; }
	}
}
