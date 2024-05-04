
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Net;
using System.Text.Json;
using Talabat.APIs.Errors;
using Talabat.APIs.Extentions;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middlewares;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories.Contract;
using Talabat.Infrastructure;
using Talabat.Infrastructure._Identity;
using Talabat.Infrastructure._Identity.DataSeed;
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

            webApplicationBuilder.Services.AddControllers() // Register Required Web APIs Services to the DI container
                                          .AddNewtonsoftJson(options=>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; // solve refernce looping
            });

            webApplicationBuilder.Services.AddSwaggerServices();

            webApplicationBuilder.Services.AddApplicationServices();

            webApplicationBuilder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
            });

            webApplicationBuilder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
            {
                options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("IdentityConnection"));
            });

            webApplicationBuilder.Services.AddSingleton<IConnectionMultiplexer>((servicesProvider) =>
            {
                var connection = webApplicationBuilder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(connection);
            });


            webApplicationBuilder.Services.AddIdentityServices(webApplicationBuilder.Configuration);

            #endregion

            var app = webApplicationBuilder.Build();

            #region Apply All Pending Migrations [Update Database] and Data Seeding

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var _dbContext = services.GetRequiredService<StoreContext>();
            var _identityDbContext = services.GetRequiredService<ApplicationIdentityDbContext>();
            // Ask CLR for Creatig Object from DbContext Explicitly

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();

            try
            {

                await StoreContextSeed.SeedAsync(_dbContext);

                await _dbContext.Database.MigrateAsync(); // Update Database

                var _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                await ApplicationIdentityDbContextSeed.SeedUserAsync(_userManager);

                await _identityDbContext.Database.MigrateAsync();

            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex);
                logger.LogError(ex, "An Error Has Been occured during apply the Migration");
            }

            #endregion

            #region Configure Kestrel Middlewares

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllers();


            #endregion

            app.Run();
        }
    }
}
