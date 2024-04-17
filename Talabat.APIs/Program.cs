
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using Talabat.APIs.Errors;
using Talabat.APIs.Extentions;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middlewares;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Infrastructure;
using Talabat.Infrastructure.Data;

namespace Talabat.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{

			var webApplicationBuilder = WebApplication.CreateBuilder(args);

			#region Configure Services

			// Add services to the DI container.

			webApplicationBuilder.Services.AddControllers(); // Register Required Web APIs Services to the DI container

			webApplicationBuilder.Services.AddSwaggerServices();
			

			webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
			});


			webApplicationBuilder.Services.AddApplicationServices();

			#endregion

			var app = webApplicationBuilder.Build();

			using var scope = app.Services.CreateScope();

			var services = scope.ServiceProvider;

			var _dbContext = services.GetRequiredService<ApplicationDbContext>();
			// Ask CLR for Creatig Object from DbContext Explicitly


			var loggerFactory = services.GetRequiredService<ILoggerFactory>();
			var logger = loggerFactory.CreateLogger<Program>();

			try
			{
				await _dbContext.Database.MigrateAsync(); // Update Database

				await ApplicationContextSeed.SeedAsync(_dbContext);
			}
			catch (Exception ex)
			{
				// Console.WriteLine(ex);
				logger.LogError(ex, "An Error Has Been occured during apply the Migration");
			}

			#region Configure Kestrel Middlewares
			
			app.UseMiddleware<ExceptionMiddleware>();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwaggerMiddlewares();
			}

			app.UseStatusCodePagesWithReExecute("/errors/{0}");

			app.UseHttpsRedirection();

			//app.UseAuthorization();

			app.UseStaticFiles();

			app.MapControllers();

			#endregion

			app.Run();
		}
	}
}
