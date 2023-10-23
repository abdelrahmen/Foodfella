using Foodfella.Core.DTOs;
using Foodfella.Core.Interfaces;
using Foodfella.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Foodfella.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RestaurantsController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;

		public RestaurantsController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		// GET: api/restaurants
		[HttpGet]
		public async Task<IActionResult> Get(int page = 1, int pageSize = 10)
		{
			var restaurants = await _unitOfWork.Restaurants
				.GetPagedAsync(page, pageSize);

			var restaurantsDTOs = restaurants
				.Select(r => RestaurantDTO.FromRestaurant(r))
				.ToList();

			return Ok(restaurantsDTOs);
		}

		//GET: /api/restaurants/by-cuisine?cuisineType=Italian&page=2&pageSize=20
		[HttpGet("by-cuisine")]
		public async Task<IActionResult> GetByCuisine(
			[FromQuery] string cuisineType,
			[FromQuery] int page = 1,
			[FromQuery] int pageSize = 10
		)
		{
			var restaurants = await _unitOfWork.Restaurants
				.FindPagedAsync(r => r.CuisineType == cuisineType, page, pageSize);

			var restaurantsDTOs = restaurants
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.Select(r => RestaurantDTO.FromRestaurant(r))
				.ToList();

			return Ok(restaurantsDTOs);
		}



		// GET: api/restaurants/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var restaurant = await _unitOfWork.Restaurants.GetByIdAsync(id);

			if (restaurant == null)
			{
				return NotFound();
			}

			var restaurantDTO = RestaurantDTO.FromRestaurant(restaurant);


			return Ok(restaurantDTO);
		}

		// POST: api/restaurants
		[HttpPost]
		[Authorize(Roles ="SuperAdmin,Admin")]
		public async Task<IActionResult> Post([FromBody] RestaurantCreateDTO restaurantDTO)
		{
			if (ModelState.IsValid)
			{
				var restaurant = new Restaurant
				{
					Name = restaurantDTO.Name,
					CuisineType = restaurantDTO.CuisineType,
					Location = restaurantDTO.Location
				};

				_unitOfWork.Restaurants.Add(restaurant);
				_unitOfWork.Complete();

				var createdRestaurantDTO = RestaurantDTO.FromRestaurant(restaurant);

				return Created($"/api/restaurants/{restaurant.Id}", createdRestaurantDTO); // 201 Created
			}

			return BadRequest(ModelState);
		}


		// PUT: api/restaurants/{id}
		[HttpPut("{id}")]
		[Authorize(Roles = "SuperAdmin,Admin")]
		public async Task<IActionResult> PutAsync(int id, [FromBody] RestaurantUpdateDTO restaurantDTO)
		{
			var existingRestaurant = await _unitOfWork.Restaurants.GetByIdAsync(id);

			if (existingRestaurant == null)
			{
				return NotFound();
			}

			existingRestaurant.Name = restaurantDTO.Name;
			existingRestaurant.CuisineType = restaurantDTO.CuisineType;
			existingRestaurant.Location = restaurantDTO.Location;

			_unitOfWork.Complete();
			return Ok(existingRestaurant);
		}

		// DELETE: api/restaurants/{id}
		[HttpDelete("{id}")]
		[Authorize(Roles = "SuperAdmin,Admin")]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var restaurant = await _unitOfWork.Restaurants.GetByIdAsync(id);

			if (restaurant == null)
			{
				return NotFound();
			}

			_unitOfWork.Restaurants.Remove(restaurant);
			_unitOfWork.Complete();
			return NoContent();
		}
	}
}
