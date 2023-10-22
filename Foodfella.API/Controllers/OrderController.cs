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
	public class OrderController : ControllerBase
	{
		private readonly IUnitOfWork unitOfWork;
		public OrderController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}



		[HttpGet("all")]
		[Authorize(Roles = "Admin,SuperAdmin")]
		public async Task<IActionResult> GetAll()
		{
			var orders = await unitOfWork.Orders.GetAllAsync();
			if (!orders.Any())
			{

			}
			var orderDTOs = orders.Select(o => OrderDTO.FromOrder(o));
			return Ok(orderDTOs);
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> GetUserOrders()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var orders = await unitOfWork.Orders.FindAsync(o => o.UserId == userId);

			if (!orders.Any())
			{
				return NotFound("this user has no orders");
			}

			var ordersDTO = orders.Select(o => OrderDTO.FromOrder(o)).ToList();
			return Ok(ordersDTO);
		}

		[HttpGet("{id}")]
		[Authorize]
		public async Task<IActionResult> GetOrderDetailsAsync(int id)
		{
			var orderDetails = await unitOfWork.OrderDetails.FindAsync(o => o.OrderId == id);

			if (!orderDetails.Any())
			{
				return NotFound("No Order Details For This Id");
			}

			if (!User.IsInRole("Admin") || !User.IsInRole("SuperAdmin"))
			{
				var order = unitOfWork.Orders.GetById(id);
				if (order.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
				{
					return Unauthorized("You are Not Authorized to View Detaul Of This Order");
				}
			}

			var orderDetailsDTO = orderDetails.Select(od => OrderDetailsDTO.FromOrderDetail(od));

			return Ok(orderDetailsDTO);
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDTO orderDTO)
		{
			try
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

				using (var transaction = unitOfWork.StartTransaction())
				{
					// Create a new order
					var order = new Order
					{
						UserId = userId,
						OrderDate = DateTime.Now,
						PaymentMethod = orderDTO.PaymentMethod,
						PickupAddress = orderDTO.PickupAddress,
						Status = "Pending",
					};

					await unitOfWork.Orders.AddAsync(order);

					// Create order details for each cart item
					foreach (var cartItemDTO in orderDTO.CartItems)
					{
						// Retrieve the corresponding menu item
						var menuItem = await unitOfWork.MenuItems.GetByIdAsync(cartItemDTO.MenuItemId);

						if (menuItem == null)
						{
							// Handle invalid menu item ID
							transaction.Rollback();
							return BadRequest($"Invalid menu item ID: {cartItemDTO.MenuItemId}");
						}

						// Create an order detail
						var orderDetail = new OrderDetail
						{
							OrderId = order.Id,
							MenuItemId = menuItem.Id,
							Quantity = cartItemDTO.Quantity,
							Price = menuItem.Price,
						};

						await unitOfWork.OrderDetails.AddAsync(orderDetail);

						// Remove the cart item from the user's cart
						var cartItem = await unitOfWork.CartItems.FindAsync(c => c.UserId == userId && c.MenuItemId == cartItemDTO.MenuItemId);

						if (cartItem.Any())
						{
							unitOfWork.CartItems.Remove(cartItem.First());
						}
					}

					unitOfWork.Complete();
					transaction.Commit();

					return Created($"/api/order/{order.Id}", OrderDTO.FromOrder(order));
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, "An error occurred while processing the order.");
			}
		}


	}
}
