using Foodfella.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodfella.Core.DTOs
{
	public class MenuDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public string Category { get; set; }
		public string Status { get; set; }

		public static MenuDTO FromMenuItem(MenuItem menu) => new MenuDTO
		{
			Id = menu.Id,
			Name = menu.Name,
			Description = menu.Description,
			Price = menu.Price,
			Category = menu.Category,
			Status = menu.Status,
		};
	}
}
