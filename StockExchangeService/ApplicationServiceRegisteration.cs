using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using StockExchangeService.Data;
using StockExchangeService.Helpers;
using StockExchangeService.Models.Dtos;
using StockExchangeService.Repositories;
using StockExchangeService.Services;
using StockExchangeService.Services.BackgroundServices;
using StockExchangeService.Services.Interfaces;
using StockExchangeService.Validators;

namespace StockExchangeService
{
    public static class ApplicationServiceRegisteration
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddSqlDBUtils(configuration)
                .AddRepositories()
                .AddAutoMapperProfile()
                .AddServices()
                .AddValidators();
            return services;
        }
        private static IServiceCollection AddSqlDBUtils(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StockDataDbContext>(
                   options =>
                   options.UseSqlServer(
                       configuration.GetConnectionString("Default"),
                       assembly => assembly.MigrationsAssembly(typeof(StockDataDbContext).Assembly.FullName)));
            return services;
        }
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
        private static IServiceCollection AddAutoMapperProfile(this IServiceCollection services)
        {
            services.AddScoped(provider => new MapperConfiguration(mc =>
            {
                mc.AddProfile(typeof(Mapping));
            })
            .CreateMapper());
            return services;
        }
        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddHostedService<StockDataBackgroundService>();
            return services;
        }
        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<OrderCreateDto>, OrderValidator>();
            return services;
        }
    }
}
