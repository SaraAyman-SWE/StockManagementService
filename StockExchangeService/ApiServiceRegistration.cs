using idunno.Authentication.Basic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace StockExchangeService
{
    public static class ApiServiceRegistration
    {
        public static IServiceCollection AddApiService(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .SwaggerRegistration(configuration)
                .ConfigureAuthenticationService(configuration)
                .AddCommonConfiguration(configuration);
            return services;
        }
        private static IServiceCollection SwaggerRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOpenApiDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Title = "Stock Exchange";
                    document.Info.Description = "ASP.NET Core web API, which works as end point for client apps";
                    document.Info.TermsOfService = "";
                };
            });
            return services;
        }
        private static IServiceCollection ConfigureAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            var hashAlgorithm = new SHA256CryptoServiceProvider();
            var username = configuration.GetSection("AuthenticationSettings:Username").Value;
            var password = configuration.GetSection("AuthenticationSettings:Password").Value;
            services.AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
                .AddBasic(options =>
                {
                    options.Realm = "Basic Authentication";
                    options.Events = new BasicAuthenticationEvents
                    {
                        OnValidateCredentials = context =>
                        {
                            var byteHash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(context.Password));
                            var hashedPassword = Convert.ToBase64String(byteHash);
                            if (context.Username == username && hashedPassword == password)
                            {
                                var claims = new[] { new Claim(ClaimTypes.NameIdentifier, context.Username, ClaimValueTypes.String, context.Options.ClaimsIssuer) };
                                context.Principal = new ClaimsPrincipal(new ClaimsIdentity(claims, context.Scheme.Name));
                                context.Success();
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            return services;
        }
        private static IServiceCollection AddCommonConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(c => c.AddPolicy("AllowOrigin", 
                options => options.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins(configuration.GetSection("ClientOrigin").Value ?? "http://localhost:4200")));

            services.AddControllers();
            services.AddHttpClient();
            services.AddSignalR();
            return services;
        }
    }
}
