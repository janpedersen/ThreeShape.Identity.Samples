using IdentityModel.AspNetCore.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using WeatherWebApi.AccessControl;
using WeatherWebApi.Infrastructure;

namespace WeatherWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IdentityConfig>(Configuration.GetSection("Identity"));
            services.AddHttpContextAccessor();
            services.AddSingleton<UserAuthorizationFactory>();
            services.AddSingleton<IWeatherForecastRepository, WeatherForecastRepository>();
            services.AddControllers();

            services
                .AddAuthentication(auth =>
                {
                    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    //should be the same as 'iss' claim in the token
                    options.Authority = Configuration["Identity:AuthEndpoint"];
                    options.RequireHttpsMetadata = bool.Parse(Configuration["Identity:RequireHttps"]);

                    //this is the name of your Api that 3S ID team should create in our system
                    //in case you want to have custom claims:
                    //i.e. if you want to limit what client can call your API and what have authorization with scopes in place
                    options.Audience = "weather";
                    //use 'api' if you don't need to require any special scopes for your api.
                    //options.Audience = "api";

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //The following commented out code is a default setup for token validation
                        //It is provided for a reference. If you want to change the behavior
                        //than you uncomment the line and change the value to what is suited for your needs

                        //RequireExpirationTime = true,
                        //RequireSignedTokens = true,
                        //RequireAudience = true,
                        //TryAllIssuerSigningKeys = true,
                        //ValidateAudience = true,
                        //ValidateIssuer = false,
                        //ValidateIssuer = true,
                        //ValidateLifetime = true,
                        //NameClaimType = "sub",
                    };
                });

            services.AddAuthorization(options =>
                {
                    //you specify that an endpoint protected by Policies.ForecastRead
                    //must have either of the following scopes: Scopes.Forecasts.ReadOnly, Scopes.Forecasts.ManageAccess
                    options.AddPolicy(Policies.ForecastRead,
                        builder => builder.RequireScope(ForecastScopes.ReadOnly, ForecastScopes.ManageAccess)
                    );
                    //you specify that an endpoint protected by Policies.ForecastManage
                    //must have Scopes.Forecasts.ManageAccess scope
                    options.AddPolicy(Policies.ForecastManage,
                        builder => builder.RequireScope(ForecastScopes.ManageAccess)
                    );
                }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}