using Foodfella.Core.Interfaces;
using Foodfella.Core.Models;
using Microsoft.EntityFrameworkCore.Storage;
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
		private IDbContextTransaction? transaction;

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

		public void StartTransaction()
		{
			if (transaction == null)
			{
				transaction = context.Database.BeginTransaction();
			}
		}

		public void CommitTransaction()
		{
			try
			{
				transaction?.Commit();
			}
			catch
			{
				RollbackTransaction();
				throw; // Re-throw the exception
			}
			finally
			{
				transaction?.Dispose();
				transaction = null;
			}
		}

		public void RollbackTransaction()
		{
			try
			{
				transaction?.Rollback();
			}
			finally
			{
				transaction?.Dispose();
				transaction = null;
			}
		}

		public int Complete()
		{
			try
			{
				return context.SaveChanges();
			}
			catch
			{
				RollbackTransaction();
				throw;
			}
		}

		public void Dispose()
		{
			context.Dispose();
			transaction?.Dispose();
		}
	}

}
