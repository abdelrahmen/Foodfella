using Foodfella.Core.DTOs;
using Foodfella.Core.Interfaces;
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
			var orderDetailsDTO = orderDetails.Select(od => OrderDetailsDTO.FromOrderDetail(od));

			return Ok(orderDetailsDTO);
		}

	}
}
