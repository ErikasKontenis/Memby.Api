using Memby.Contracts.Repositories;
using Memby.Contracts.Services;
using Memby.Data.DbContexts;
using Memby.Data.Extensions;
using Memby.Data.Repositories;
using Memby.Middlewares;
using Memby.Services.Jwt;
using Memby.Services.Security;
using Memby.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Memby
{
    public class Startup
    {
        private readonly Container _container = new Container();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<MembyDbContext>(options => options.UseMySql(Configuration.GetConnectionString("DatabaseConnectionString")));

            ConfigureSimpleInjector(services);
            ConfigureJwt(services);
            ConfigureSecurity(services);
            ConfigureSwagger(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
            app.UseAuthentication();
            app.UseMvc();

            ConfigureContainer(app);
            ConfigureMigrations(app);
            ConfigureSwagger(app);
        }

        private void ConfigureSimpleInjector(IServiceCollection services)
        {
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(_container));
            services.AddSingleton<IViewComponentActivator>(
                new SimpleInjectorViewComponentActivator(_container));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.EnableSimpleInjectorCrossWiring(_container);
            services.UseSimpleInjectorAspNetRequestScoping(_container);
        }

        private void ConfigureContainer(IApplicationBuilder app)
        {
            // Add application presentation components:
            _container.RegisterMvcControllers(app);
            _container.RegisterMvcViewComponents(app);

            // Add application services:
            _container.Register(typeof(IUsersService), typeof(UsersService));
            _container.Register(typeof(ISecurityService), typeof(SecurityService));

            // Automatically register generic repositories by contract
            _container.Register(typeof(IRepository<>), typeof(Repository<>));
            _container.Register(typeof(IUsersRepository), typeof(UsersRepository));

            _container.AutoCrossWireAspNetComponents(app);
            _container.Verify();
        }

        private void ConfigureJwt(IServiceCollection services)
        {
            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtAppSettingOptions["issuerSigningKey"]));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = issuerSigningKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
            });

            services.AddSingleton<IJwtFactory, JwtFactory>();
        }

        private void ConfigureSecurity(IServiceCollection services)
        {
            // Get options from app settings
            var securityOptions = Configuration.GetSection(nameof(SecurityOptions));

            // Configure JwtIssuerOptions
            services.Configure<SecurityOptions>(options =>
            {
                options.PasswordSalt = securityOptions[nameof(SecurityOptions.PasswordSalt)];
            });
        }

        private void ConfigureMigrations(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                if (!serviceScope.ServiceProvider.GetService<MembyDbContext>().AllMigrationsApplied())
                {
                    serviceScope.ServiceProvider.GetService<MembyDbContext>().Database.Migrate();
                }
            }
        }

        private void ConfigureSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Memby", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme { In = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)], Description = "Please enter JWT with Bearer into field", Name = "Authorization", Type = "apiKey" });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    {
                        "Bearer", Enumerable.Empty<string>()
                    },
                });
            });
        }
    }
}
