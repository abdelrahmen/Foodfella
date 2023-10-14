using Foodfella.Core.Interfaces;
using Foodfella.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodfella.EF.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext context;

		public UnitOfWork(AppDbContext context)
		{
			this.context = context;
			this.Restaurants = new BaseRepository<Restaurant>(context);
			this.CartItems = new BaseRepository<CartItem>(context);
			this.MenuItems = new BaseRepository<MenuItem>(context);
			this.Orders = new BaseRepository<Order>(context);
			this.OrderDetails = new BaseRepository<OrderDetail>(context);
		}
		public IBaseRepository<Restaurant> Restaurants { get; private set; }

		public IBaseRepository<MenuItem> MenuItems { get; private set; }

		public IBaseRepository<CartItem> CartItems { get; private set; }

		public IBaseRepository<Order> Orders { get; private set; }

		public IBaseRepository<OrderDetail> OrderDetails { get; private set; }

		public int Complete() => context.SaveChanges();

		public void Dispose() => context.Dispose();

	}
}
