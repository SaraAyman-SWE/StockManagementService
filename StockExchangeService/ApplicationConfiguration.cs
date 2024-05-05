using Microsoft.EntityFrameworkCore;
using StockExchangeService.Data;
using StockExchangeService.Hubs;

namespace StockExchangeService
{
    public static class ApplicationConfiguration
    {
        public static IApplicationBuilder ConfigureApplicationStart(this IApplicationBuilder app, IWebHostEnvironment env, StockDataDbContext context, IConfiguration configuration)
        {
            context.Database.Migrate();
            app.
                UseDeveloperExceptionPage().
                ConfigureSwagger().
                ConfigureCrossOriginUsing().
                UseHttpsRedirection().
                UseRouting().
                UseAuthentication().
                UseAuthorization().
                ConfigureEndPoints();

            return app;
        }
        private static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseOpenApi();
            app.UseSwaggerUi();
            return app;
        }

        private static IApplicationBuilder ConfigureCrossOriginUsing(this IApplicationBuilder app)
        {
            app.UseCors("AllowOrigin");
            return app;
        }

        private static IApplicationBuilder ConfigureEndPoints(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints => 
            {
                endpoints.MapHub<StockTickerHub>("/realtimestocks");
                endpoints.MapControllers().RequireAuthorization();
            });
            return app;
        }
    }
}
