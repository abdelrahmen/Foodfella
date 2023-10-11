using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foodfella.Core.Models
{
	public class ApplicationUser: IdentityUser
	{
		[Required, MaxLength(150)]
		public string FullName { get; set; }

		[MaxLength(150)]
		public string address { get; set; }
	}
}
