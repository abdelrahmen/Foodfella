using Foodfella.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodfella.Core.DTOs
{
	public class RestaurantDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string CuisineType { get; set; }
		public string Location { get; set; }
		public double AverageRating { get; set; }

		public static RestaurantDTO FromRestaurant(Restaurant restaurant) => new RestaurantDTO
		{
			Id = restaurant.Id,
			Name = restaurant.Name,
			CuisineType = restaurant.CuisineType,
			Location = restaurant.Location,
			AverageRating = restaurant.AverageRating
		};
	}
}
