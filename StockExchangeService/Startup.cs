using StockExchangeService.Data;

namespace StockExchangeService
{
    public class Startup
    {
        private IConfiguration _configuration { get; }
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddApplicationService(_configuration)
                .AddApiService(_configuration);
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env, StockDataDbContext context)
        {
            app.ConfigureApplicationStart(env, context, _configuration);
        }
    }
}
