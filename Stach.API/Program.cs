using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stach.API.Errors;
using Stach.API.Extensions;
using Stach.API.Helpers;
using Stach.API.Middlewares;
using Stach.Domain.Repositories;
using Stach.Repository;
using Stach.Repository.Data;
using Stach.Repository.Identity;
using StackExchange.Redis;

namespace Stach.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddSwaggerServices();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });

            builder.Services.AddApplicationServices();
            #endregion

            var app = builder.Build();

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var _dbContext = services.GetRequiredService<ApplicationDbContext>();

            var _identityDbContext = services.GetRequiredService<AppIdentityDbContext>();

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbContext.Database.MigrateAsync(); // Update-Database
                await ApplicationDbContextSeed.SeedAsync(_dbContext); // Data Seeding

                await _identityDbContext.Database.MigrateAsync(); // Update-Database
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has occured while applying the migration");
            }

            #region Configure Kestrel Middlewares
            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleware();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();

            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
