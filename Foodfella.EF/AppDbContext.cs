using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodfella.EF
{
	public class AppDbContext : IdentityDbContext //add the user
	{
		AppDbContext(DbContextOptions options) : base(options) { }

	}
}
