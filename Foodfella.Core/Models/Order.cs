using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodfella.Core.Models
{
	public class Order
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

		[ForeignKey(nameof(UserId))]
		public ApplicationUser User { get; set; }

		public List<OrderDetail> OrderDetails { get; set; }
	}
}
