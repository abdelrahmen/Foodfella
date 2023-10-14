using Foodfella.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodfella.Core.Interfaces
{
	public interface IUnitOfWork: IDisposable
	{
		IBaseRepository<Restaurant> Restaurants { get; }
		IBaseRepository<MenuItem> MenuItems { get; }
		IBaseRepository<CartItem> CartItems { get; }
		IBaseRepository<Order> Orders { get; }
		IBaseRepository<OrderDetail> OrderDetails { get; }

		int Complete();
	}
}
