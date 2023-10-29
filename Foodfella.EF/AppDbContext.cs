using Foodfella.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodfella.EF
{
	public class AppDbContext : IdentityDbContext<ApplicationUser>
	{
		public AppDbContext(DbContextOptions options) : base(options) { }

		public AppDbContext() { }

		public DbSet<Restaurant> Restaurants { get; set; }
		public DbSet<MenuItem> MenuItems { get; set; }
		public DbSet<CartItem> CartItems { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }


		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<IdentityRole>().HasData(SeedRoles);
			builder.Entity<ApplicationUser>().HasData(SeedSuperAdmin());
		}

		private IdentityRole[] SeedRoles = new IdentityRole[]
		{
			new IdentityRole { Id = "1", Name = "SuperAdmin", NormalizedName = "SUPERADMIN" },
			new IdentityRole { Id = "2", Name = "Admin", NormalizedName = "ADMIN" },
			new IdentityRole { Id = "3", Name = "Customer", NormalizedName = "CUSTOMER" },
		};

		private ApplicationUser SeedSuperAdmin()
		{
			var hasher = new PasswordHasher<ApplicationUser>();
			return new ApplicationUser
			{
				Id = "1",
				UserName = "TemporaryUsername",
				NormalizedUserName = "TEMPORARY-USERNAME",
				Email = "TemporaryEmail@example.com",
				NormalizedEmail = "TEMPORARYEMAIL@EXAMPLE.COM",
				FullName = "Temporary Full Name",
				address = "Temporary Address",
				EmailConfirmed = true,
				PasswordHash = hasher.HashPassword(null, "TemporaryPassword"),
				SecurityStamp = string.Empty
			};
		}
	}
}
