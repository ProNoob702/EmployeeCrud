using EmployeeCrud.Data;
using EmployeeCrud.Web.Models;
using EmployeeCrud.Web.Profiles;
using EmployeeCrud.Web.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EmployeeCrud.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration config)
        {
            // Register DbContext
            services.AddDbContext<DataContext>(
                options => options.UseSqlServer(config.GetConnectionString("SQLDatabase"))
            );

            // Register custom services
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IJwtAuthManager, JwtAuthManager>();

            // Register configs
            var jwtTokenConfig = config.GetSection("jwtTokenConfig").Get<JwtTokenConfig>();
            services.AddSingleton(jwtTokenConfig);

            // Register AutoMapper profiles
            services.AddAutoMapper(typeof(AutoMapperProfiles));

            ConfigureIdentity(services);

            ConfigureAuth(services, jwtTokenConfig);
        }

        private static void ConfigureIdentity(IServiceCollection services)
        {
            // Identity stuff 
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();
        }

        private static void ConfigureAuth(IServiceCollection services, JwtTokenConfig jwtTokenConfig)
        {
            // Auth stuff - JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtTokenConfig.Audience,
                    ValidIssuer = jwtTokenConfig.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenConfig.Secret))
                };
            });
        }
    }
}
