using Foodfella.API.Controllers;
using Foodfella.Core.DTOs;
using Foodfella.Core.Interfaces;
using Foodfella.Core.Models;
using Foodfella.EF;
using Foodfella.EF.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodfella.tests.Foodfella.API.Controllers
{
	public class RestaurantsControllerTests
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly AppDbContext context;
		private readonly RestaurantsController RestaurantsController;

		public RestaurantsControllerTests()
		{
			var options = new DbContextOptionsBuilder<AppDbContext>()
			   .UseInMemoryDatabase(databaseName: "FoodfellaTestDB")
			   .Options;
			this.context = new AppDbContext(options);
			this.unitOfWork = new UnitOfWork(context);
			this.RestaurantsController = new RestaurantsController(unitOfWork);
		}

		[Fact]
		public async Task Get_ReturnsOkResult()
		{
			// Arrange: Add some sample restaurants to the in-memory database
			var restaurant1 = new Restaurant
			{
				Name = "Restaurant 1",
				CuisineType = "Italian",
				Location = "Location 1"
			};

			var restaurant2 = new Restaurant
			{
				Name = "Restaurant 2",
				CuisineType = "Mexican",
				Location = "Location 2"
			};

			unitOfWork.Restaurants.Add(restaurant1);
			unitOfWork.Restaurants.Add(restaurant2);
			unitOfWork.Complete();

			// Act: Call the Get action
			var result = await RestaurantsController.Get();

			// Assert: Check if the action returns an OkObjectResult
			var okResult = Assert.IsType<OkObjectResult>(result);
			var restaurants = Assert.IsType<List<RestaurantDTO>>(okResult.Value);

			// Assert: Check if the result contains the added restaurants
			Assert.Equal(2, restaurants.Count);
			Assert.Contains(restaurants, r => r.Name == "Restaurant 1");
			Assert.Contains(restaurants, r => r.Name == "Restaurant 2");
		}

		[Fact]
		public async Task Get_WithValidId_ReturnsOkResult()
		{
			// Arrange: Add a sample restaurant to the in-memory database
			var restaurant = new Restaurant
			{
				Name = "Sample Restaurant",
				CuisineType = "Chinese",
				Location = "Sample Location"
			};

			unitOfWork.Restaurants.Add(restaurant);
			unitOfWork.Complete();

			// Act: Call the Get action with the added restaurant's ID
			var result = await RestaurantsController.Get(restaurant.Id);

			// Assert: Check if the action returns an OkObjectResult
			var okResult = Assert.IsType<OkObjectResult>(result);
			var restaurantDTO = Assert.IsType<RestaurantDTO>(okResult.Value);

			// Assert: Check if the result matches the added restaurant
			Assert.Equal(restaurant.Name, restaurantDTO.Name);
			Assert.Equal(restaurant.CuisineType, restaurantDTO.CuisineType);
			Assert.Equal(restaurant.Location, restaurantDTO.Location);
		}

		[Fact]
		public async Task PutAsync_WithValidIdAndValidData_ReturnsOkResult()
		{
			// Arrange: Add a sample restaurant to the in-memory database
			var restaurant = new Restaurant
			{
				Name = "Sample Restaurant",
				CuisineType = "Chinese",
				Location = "Sample Location"
			};

			unitOfWork.Restaurants.Add(restaurant);
			unitOfWork.Complete();

			// Act: Call the PutAsync action with updated restaurant data
			var updatedName = "Updated Restaurant";
			var updatedCuisine = "Italian";
			var updatedLocation = "Updated Location";

			var updatedRestaurantDTO = new RestaurantUpdateDTO
			{
				Name = updatedName,
				CuisineType = updatedCuisine,
				Location = updatedLocation
			};

			var result = await RestaurantsController.PutAsync(restaurant.Id, updatedRestaurantDTO);

			// Assert: Check if the action returns an OkObjectResult
			var okResult = Assert.IsType<OkObjectResult>(result);
			var updatedRestaurant = Assert.IsType<Restaurant>(okResult.Value);

			// Assert: Check if the updated restaurant matches the provided data
			Assert.Equal(updatedName, updatedRestaurant.Name);
			Assert.Equal(updatedCuisine, updatedRestaurant.CuisineType);
			Assert.Equal(updatedLocation, updatedRestaurant.Location);
		}

		[Fact]
		public async Task DeleteAsync_WithValidId_ReturnsNoContent()
		{
			// Arrange: Add a sample restaurant to the in-memory database
			var restaurant = new Restaurant
			{
				Name = "Sample Restaurant",
				CuisineType = "Chinese",
				Location = "Sample Location"
			};

			unitOfWork.Restaurants.Add(restaurant);
			unitOfWork.Complete();

			// Act: Call the DeleteAsync action with the restaurant's ID
			var result = await RestaurantsController.DeleteAsync(restaurant.Id);

			// Assert: Check if the action returns NoContent
			Assert.IsType<NoContentResult>(result);

			// Assert: Check if the restaurant has been deleted
			var deletedRestaurant = await unitOfWork.Restaurants.GetByIdAsync(restaurant.Id);
			Assert.Null(deletedRestaurant);
		}

	}
}
