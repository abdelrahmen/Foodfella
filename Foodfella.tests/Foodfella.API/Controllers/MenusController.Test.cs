using Foodfella.API.Controllers;
using Foodfella.Core.DTOs;
using Foodfella.Core.Interfaces;
using Foodfella.Core.Models;
using Foodfella.EF;
using Foodfella.EF.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodfella.tests.Foodfella.API.Controllers
{
	public class MenusControllerTests
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly AppDbContext context;
		private readonly MenusController menusController;
		public MenusControllerTests()
		{
			var options = new DbContextOptionsBuilder<AppDbContext>()
			   .UseInMemoryDatabase(databaseName: "FoodfellaTestDB")
			   .Options;
			this.context = new AppDbContext(options);
			this.unitOfWork = new UnitOfWork(context);
			this.menusController = new MenusController(unitOfWork);
		}

		[Fact]
		public async Task GetById_ReturnsOkResult()
		{
			// Arrange: Add a menu item to the in-memory database
			var restaurant = new Restaurant
			{
				Name = "Test",
				AverageRating = 1,
				CreatedAt = DateTime.Now,
				CuisineType = "test",
				Location = "test",
			};

			unitOfWork.Restaurants.Add(restaurant);

			var menuItem = new MenuItem
			{
				Name = "Test Menu Item",
				Category = "Test Category",
				Description = "Test Description",
				Price = 10.0m,
				RestaurantId = restaurant.Id,
				Status = "available"
			};

			unitOfWork.MenuItems.Add(menuItem);
			unitOfWork.Complete();

			// Act: Call the GetById action with the menu item's ID
			var result = await menusController.GetById(menuItem.Id);

			// Assert: Check if the action returns an OkResult
			var okResult = Assert.IsType<OkObjectResult>(result);
			var menuDTO = Assert.IsType<MenuDTO>(okResult.Value);
			Assert.Equal(menuItem.Id, menuDTO.Id);
		}

		// For GetByRestaurantId method
		[Fact]
		public async Task GetByRestaurantId_ReturnsOkResult()
		{
			// Arrange: Add a restaurant and its menu items to the in-memory database
			var restaurant = new Restaurant
			{
				Name = "Test Restaurant",
				AverageRating = 3,
				CreatedAt = DateTime.Now,
				CuisineType = "test",
				Location = "test",
			};

			unitOfWork.Restaurants.Add(restaurant);

			var menuItem1 = new MenuItem
			{
				Name = "Item 1",
				Category = "Category 1",
				Description = "Description 1",
				Price = 10.0m,
				RestaurantId = restaurant.Id,
				Status = "available"
			};

			var menuItem2 = new MenuItem
			{
				Name = "Item 2",
				Category = "Category 2",
				Description = "Description 2",
				Price = 15.0m,
				RestaurantId = restaurant.Id,
				Status = "available"
			};

			unitOfWork.MenuItems.Add(menuItem1);
			unitOfWork.MenuItems.Add(menuItem2);
			unitOfWork.Complete();

			// Act: Call the GetByRestaurantId action with the restaurant's ID
			var result = await menusController.GetByRestaurantId(restaurant.Id);

			// Assert: Check if the action returns an OkResult
			var okResult = Assert.IsType<OkObjectResult>(result);
			var menuItems = Assert.IsType<List<MenuDTO>>(okResult.Value);
			Assert.Equal(2, menuItems.Count);
		}

		// For Create method
		[Fact]
		public async Task Create_ReturnsCreatedResult()
		{
			// Arrange:
			//create a restaurant
			var restaurant = new Restaurant
			{
				Name = "Test",
				AverageRating = 1,
				CreatedAt = DateTime.Now,
				CuisineType = "test",
				Location = "test",
			};

			unitOfWork.Restaurants.Add(restaurant);
			// Create a MenuCreateDTO
			var menuItemDTO = new MenuCreateDTO
			{
				Name = "New Menu Item",
				Category = "New Category",
				Description = "New Description",
				Price = 12.0m,
				RestaurantId = restaurant.Id,
				Status = "available"
			};

			// Act: Call the Create action with the menu item DTO
			var result = await menusController.Create(menuItemDTO);

			// Assert: Check if the action returns a CreatedResult
			var createdResult = Assert.IsType<CreatedAtActionResult>(result);
			var createdMenuItemDTO = Assert.IsType<MenuDTO>(createdResult.Value);
			Assert.Equal(menuItemDTO.Name, createdMenuItemDTO.Name);
		}

		// For Update method
		[Fact]
		public async Task Update_ReturnsOkResult()
		{
			// Arrange: Add a menu item to the in-memory database
			var restaurant = new Restaurant
			{
				Name = "Test",
				AverageRating = 1,
				CreatedAt = DateTime.Now,
				CuisineType = "test",
				Location = "test",
			};

			unitOfWork.Restaurants.Add(restaurant);

			var menuItem = new MenuItem
			{
				Name = "Test Menu Item",
				Category = "Test Category",
				Description = "Test Description",
				Price = 10.0m,
				RestaurantId = restaurant.Id,
				Status = "available"
			};

			unitOfWork.MenuItems.Add(menuItem);
			unitOfWork.Complete();

			// Create a MenuUpdateDTO with updated values
			var updatedMenuDTO = new MenuUpdateDTO
			{
				Name = "Updated Name",
				Description = "Updated Description",
				Price = 15.0m,
				Category = "Updated Category",
				RestaurantId = restaurant.Id,
				Status = "updated"
			};

			// Act: Call the Update action with the menu item ID and the updated DTO
			var result = await menusController.Update(menuItem.Id, updatedMenuDTO);

			// Assert: Check if the action returns an OkResult with updated values
			var okResult = Assert.IsType<OkObjectResult>(result);
			var updatedMenu = Assert.IsType<MenuDTO>(okResult.Value);
			Assert.Equal(updatedMenuDTO.Name, updatedMenu.Name);
			Assert.Equal(updatedMenuDTO.Description, updatedMenu.Description);
			Assert.Equal(updatedMenuDTO.Price, updatedMenu.Price);
			Assert.Equal(updatedMenuDTO.Category, updatedMenu.Category);
			Assert.Equal(updatedMenuDTO.Status, updatedMenu.Status);
		}


		// For Delete method
		[Fact]
		public IActionResult Delete_ReturnsNoContentResult()
		{
			// Arrange: Add a menu item to the in-memory database
			var restaurant = new Restaurant
			{
				Name = "Test",
				AverageRating = 1,
				CreatedAt = DateTime.Now,
				CuisineType = "test",
				Location = "test",
			};

			unitOfWork.Restaurants.Add(restaurant);

			var menuItem = new MenuItem
			{
				Name = "Test Menu Item",
				Category = "Test Category",
				Description = "Test Description",
				Price = 10.0m,
				RestaurantId = restaurant.Id,
				Status = "available"
			};

			unitOfWork.MenuItems.Add(menuItem);
			unitOfWork.Complete();

			// Act: Call the Delete action with the menu item ID
			var result = menusController.Delete(menuItem.Id);

			// Assert: Check if the action returns a NoContentResult
			var noContentResult = Assert.IsType<NoContentResult>(result);
			return noContentResult;
		}
	}
}
