using Foodfella.Core.DTOs;
using Foodfella.Core.Interfaces;
using Foodfella.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Foodfella.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private readonly IUnitOfWork unitOfWork;

		public CartController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}
		// GET: api/cart
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> GetAsync()
		{
			var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var cartItems = await unitOfWork.CartItems.FindAsync(c => c.UserId == currentUserId);

			if (!cartItems.Any())
			{
				return NotFound("Cart is empty.");
			}
			var cartItemDTOs = cartItems.Select(c => CartItemDTO.FromCartItem(c)).ToList();

			return Ok(cartItemDTOs);
		}

		// GET: api/cart/{id}
		[HttpGet("{id}")]
		[Authorize]
		public async Task<IActionResult> GetById(int id)
		{
			var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var cartItem = await unitOfWork.CartItems.GetByIdAsync(id);

			if (cartItem == null)
			{
				return NotFound("CartItem not found.");
			}

			if (cartItem.UserId != currentUserId)
			{
				return Unauthorized("You do not have permission to access this cart item.");
			}

			var cartItemDTO = CartItemDTO.FromCartItem(cartItem);

			return Ok(cartItemDTO);
		}

		// POST: api/cart
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> PostAsync([FromBody] CartItemCreateDTO cartItemDTO)
		{
			if (cartItemDTO == null)
			{
				return BadRequest("Invalid cart item data.");
			}

			if (ModelState.IsValid)
			{
				var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				var currentCartItem = await unitOfWork.CartItems.FindAsync(c => c.UserId == currentUserId && c.MenuItemId == cartItemDTO.MenuItemId);
				if (currentCartItem == null || !currentCartItem.Any())
				{
					var newCartItem = new CartItem
					{
						UserId = currentUserId,
						MenuItemId = cartItemDTO.MenuItemId,
						CreatedAt = DateTime.Now,
						Price = cartItemDTO.Price,
						Quantity = cartItemDTO.Quantity,
					};
					await unitOfWork.CartItems.AddAsync(newCartItem);
					unitOfWork.Complete();
					return Created($"/api/cart/{newCartItem.Id}", CartItemDTO.FromCartItem(newCartItem));
				}
				else
				{
					var existingCartItem = currentCartItem.First();
					existingCartItem.Quantity += cartItemDTO.Quantity;
					unitOfWork.Complete();
					return Created($"/api/cart/{existingCartItem.Id}", CartItemDTO.FromCartItem(existingCartItem));
				}
			}

			return BadRequest(ModelState);
		}

		// PUT: api/cart/{id}
		[HttpPut("{id}")]
		[Authorize]
		public IActionResult Put(int id, [FromBody] CartItemUpdateDTO cartItemDTO)
		{
			if (cartItemDTO == null)
			{
				return BadRequest("Invalid cart item update data.");
			}

			var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var existingCartItem = unitOfWork.CartItems.GetById(id);

			if (existingCartItem == null)
			{
				return NotFound("Cart item not found.");
			}

			if (existingCartItem.UserId != currentUserId)
			{
				return Forbid("You don't have permission to update this cart item.");
			}

			existingCartItem.Quantity = cartItemDTO.Quantity;
			existingCartItem.Price = cartItemDTO.Price;
			existingCartItem.UpdatedAt = DateTime.Now;

			unitOfWork.Complete();

			return Ok(CartItemDTO.FromCartItem(existingCartItem));
		}


		// DELETE: api/cart/{id}
		[HttpDelete("{id}")]
		[Authorize]
		public IActionResult Delete(int id)
		{
			var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var cartItem = unitOfWork.CartItems.GetById(id);

			if (cartItem == null)
			{
				return NotFound("Cart item not found.");
			}

			if (cartItem.UserId != currentUserId)
			{
				return Forbid("You don't have permission to delete this cart item.");
			}

			unitOfWork.CartItems.Remove(cartItem);
			unitOfWork.Complete();

			return NoContent();
		}


		// DELETE: api/cart/delete-all
		[HttpDelete("delete-all")]
		[Authorize]
		public IActionResult DeleteAll()
		{
			var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var userCartItems = unitOfWork.CartItems.Find(c => c.UserId == currentUserId);

			if (!userCartItems.Any())
			{
				return NotFound("No cart items to delete.");
			}

			unitOfWork.CartItems.RemoveRange(userCartItems);
			unitOfWork.Complete();

			return NoContent();
		}

	}
}
