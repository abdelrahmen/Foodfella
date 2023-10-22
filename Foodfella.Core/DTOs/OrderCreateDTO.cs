using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodfella.Core.DTOs
{
	public class OrderCreateDTO
	{
		[Required]
		public List<CartItemCreateDTO> CartItems { get; set; }
		[Required]
		[MaxLength(200)]
		public string PickupAddress { get; set; }
		[Required]
		[MaxLength(50)]
		public string PaymentMethod { get; set; }
	}
}
