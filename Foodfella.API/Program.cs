using Foodfella.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Text;

namespace Foodfella.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Configuration.AddJsonFile("appsettings.json");
			var config = builder.Configuration;

			//logging config
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Information()
				.WriteTo.File("Log/HTTPLog-.txt", rollingInterval: RollingInterval.Infinite)
				.CreateLogger();

			builder.Host.UseSerilog();

			//JWT Config
			builder.Services.AddAuthentication(o =>
			{
				o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}
			)
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateLifetime = true,
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidIssuer = config["Jwt:Issuer"],
					ValidAudience = config["Jwt:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
				};
			});

			//EF Config
			var connectionString = config.GetConnectionString("local");

			builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
				connectionString,
				c => c.MigrationsAssembly(typeof(AppDbContext).FullName)
				));

			builder.Services.AddControllers();

			//swagger config
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseSerilogRequestLogging();


			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}