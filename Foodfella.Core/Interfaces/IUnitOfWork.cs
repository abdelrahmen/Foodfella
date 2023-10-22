using Foodfella.Core.Models;
using Microsoft.EntityFrameworkCore.Storage;
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

		IDbContextTransaction StartTransaction();

		void CommitTransaction();

		void RollbackTransaction();

		void Dispose();
	}
}
