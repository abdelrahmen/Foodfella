using Foodfella.Core.DTO;
using Foodfella.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Foodfella.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IConfiguration config;

		public AccountController(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			IConfiguration configuration
			)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			this.config = configuration;
		}


		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterDTO model)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser
				{
					UserName = model.UserName,
					Email = model.Email,
					FullName = model.FullName
				};

				var result = await _userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					await _userManager.AddToRoleAsync(user, "Patient");
					return Ok("Registration successful");
				}
				return BadRequest(result.Errors.Select(e=>e.Description));
			}
			return BadRequest(ModelState);
		}

		[HttpPost("register-admin")]
		[Authorize(Roles = "SuperAdmin")] 
		public async Task<IActionResult> RegisterAdmin(RegisterDTO model)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser
				{
					UserName = model.UserName,
					Email = model.Email,
					FullName = model.FullName
				};

				var result = await _userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					// Assign the "Admin" role to the newly registered user
					await _userManager.AddToRoleAsync(user, "Admin");
					return Ok("Admin registration successful");
				}
				return BadRequest(result.Errors.Select(e=>e.Description));
			}
			return BadRequest(ModelState);
		}


		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginDTO model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);

				if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
				{
					var token = await GenerateJwtTokenAsync(user);

					return Ok(new { token });
				}

				return Unauthorized("Invalid login attempt.");
			}

			return BadRequest(ModelState);
		}

		[HttpPost("logout")]
		[Authorize]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return Ok("Logout successful");
		}

		[HttpPut("edit-profile")]
		[Authorize]
		public async Task<IActionResult> EditProfile(EditProfileDTO model)
		{
			if (ModelState.IsValid)
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				var user = await _userManager.FindByIdAsync(userId);

				if (user != null)
				{
					user.Email = model.Email;
					user.FullName = model.FullName;

					if (!string.IsNullOrEmpty(model.NewPassword))
					{
						// Change the password if a new password is provided
						var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

						if (!changePasswordResult.Succeeded)
						{
							return BadRequest(changePasswordResult.Errors.Select(e => e.Description));
						}
					}

					var result = await _userManager.UpdateAsync(user);

					if (result.Succeeded)
					{
						return Ok("Profile updated successfully");
					}

					return BadRequest(result.Errors.Select(e => e.Description));
				}

				return NotFound("User not found");
			}

			return BadRequest(ModelState);
		}

		private async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
		{
			var claims = new List<Claim>
			{
			new Claim(JwtRegisteredClaimNames.Sub, user.Email),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			new Claim(ClaimTypes.NameIdentifier, user.Id)
			};
			var roles = await _userManager.GetRolesAsync(user);
			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
			}

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expires = DateTime.Now.AddMinutes(30);

			var token = new JwtSecurityToken(
				config["Jwt:Issuer"],
				config["Jwt:Audience"],
				claims,
				expires: expires,
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
