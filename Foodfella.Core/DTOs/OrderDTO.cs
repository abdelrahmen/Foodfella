using Foodfella.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodfella.Core.DTOs
{
	public class OrderDTO
	{
		public int Id { get; set; }

		[Required]
		public string UserId { get; set; }

		[Required]
		public DateTime OrderDate { get; set; }

		[Required]
		[MaxLength(50)]
		public string PaymentMethod { get; set; }

		[Required]
		[MaxLength(200)]
		public string PickupAddress { get; set; }

		[Required]
		[MaxLength(15)]
		public string Status { get; set; }

		public static OrderDTO FromOrder(Order order) => new OrderDTO
		{
			Id = order.Id,
			UserId = order.UserId,
			OrderDate = order.OrderDate,
			PaymentMethod = order.PaymentMethod,
			PickupAddress = order.PickupAddress,
			Status = order.Status,
		};
	}
}
