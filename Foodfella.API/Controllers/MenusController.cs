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
	public class MenusController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;

		public MenusController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		// GET: api/menus/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var menuItem = await _unitOfWork.MenuItems.GetByIdAsync(id);
			if (menuItem == null)
			{
				return NotFound();
			}

			var menuDTO = MenuDTO.FromMenuItem(menuItem);
			return Ok(menuDTO);
		}

		// GET: api/menus/restaurant/{restaurantId}
		[HttpGet("restaurant/{id}")]
		public async Task<IActionResult> GetByRestaurantId(int restaurantId)
		{
			var menu = await _unitOfWork.MenuItems.FindAsync(m => m.RestaurantId == restaurantId);
			if (menu == null)
			{
				return NotFound();
			}

			var menuItems = menu.Select(m => MenuDTO.FromMenuItem(m)).ToList();

			return Ok(menuItems);
		}

		// POST: api/menus
		[HttpPost]
		[Authorize(Roles = "SuperAdmin,Admin")]
		public async Task<IActionResult> Create([FromBody] MenuCreateDTO menuItemDTO)
		{
			if (ModelState.IsValid)
			{
				var restaurant = _unitOfWork.Restaurants.GetById(menuItemDTO.RestaurantId); 
				if (restaurant == null)
				{
					return BadRequest($"No restaurant found for the provided restaurant id: {menuItemDTO.RestaurantId}");
				}

				var menuItem = new MenuItem
				{
					Name = menuItemDTO.Name,
					Category = menuItemDTO.Category,
					Description = menuItemDTO.Description,
					CreatedAt = DateTime.Now,
					Price = menuItemDTO.Price,
					RestaurantId = menuItemDTO.RestaurantId,
					Status = menuItemDTO.Status,
				};

				await _unitOfWork.MenuItems.AddAsync(menuItem);
				_unitOfWork.Complete();

				var createdMenuItemDTO = MenuDTO.FromMenuItem(menuItem);

				return CreatedAtAction($"/api/menus/{menuItem.Id}", createdMenuItemDTO);
			}

			return BadRequest(ModelState);
		}

		// PUT: api/menus/{id}
		[HttpPut("{id}")]
		[Authorize(Roles = "SuperAdmin,Admin")]
		public async Task<IActionResult> Update(int id, [FromBody] MenuUpdateDTO menuDTO)
		{
			var existingMenu = await _unitOfWork.MenuItems.GetByIdAsync(id);
			if (existingMenu == null)
			{
				return NotFound();
			}

			existingMenu.Name = menuDTO.Name;
			existingMenu.Description = menuDTO.Description;
			existingMenu.Price = menuDTO.Price;
			existingMenu.Category = menuDTO.Category;
			existingMenu.RestaurantId = menuDTO.RestaurantId;
			existingMenu.Status = menuDTO.Status;
			existingMenu.UpdatedAt = DateTime.Now;

			_unitOfWork.Complete();

			var updatedMenuDTO = MenuDTO.FromMenuItem(existingMenu);
			return Ok(updatedMenuDTO);
		}

		// DELETE: api/menus/{id}
		[HttpDelete("{id}")]
		[Authorize(Roles = "SuperAdmin,Admin")]
		public IActionResult Delete(int id)
		{
			var menu = _unitOfWork.MenuItems.GetById(id);
			if (menu == null)
			{
				return NotFound();
			}

			_unitOfWork.MenuItems.Remove(menu);
			_unitOfWork.Complete();

			return NoContent();
		}
	}
}
