using Foodfella.Core.DTOs;
using Foodfella.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Foodfella.API.Controllers
{
	public class OrderUpdateDTO
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(15)]
		public string Status { get; set; }
	}
}