using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using OnlineOrdering.Common.Validations;
using OnlineOrdering.Data;
using OnlineOrdering.Data.DatabaseContext;
using OnlineOrdering.Data.Interfaces;
using OnlineOrdering.Services;
using OnlineOrdering.Services.Interfaces;

namespace OnlineOrdering.API.Extensions
{
    public static class ServiceExtentions
    {
        public static void AddControllersExtension(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(newtonsoft =>
            {
                 newtonsoft.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
        }

        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swivel Tests", Version = "v1.0.0" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "Using the Authorization header with the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchema);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                });
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }

        public static void AddDatabaseContextExtension(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContextPool<OnlineOrderingDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultDatabase"));
            });
        }

        public static void AddFluentValidationExtension(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new ValidationFilter());
            }).AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<CreateCustomerRequestValidator>();
                options.RegisterValidatorsFromAssemblyContaining<CreateProductRequestValidator>();
                options.RegisterValidatorsFromAssemblyContaining<CreateOrderHeaderRequestValidator>();
                options.RegisterValidatorsFromAssemblyContaining<UpdateOrderStatusRequestValidator>();
            });
        }

        public static void AddAuthenticationExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = $"https://{configuration["Auth0:Domain"]}/";
                options.Audience = configuration["Auth0:Audience"];
            });
        }

        public static void AddTransientRepositoriesAndServicesExtension(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddTransient<IProductsRepository, ProductsRepository>();
            services.AddTransient<IProductsService, ProductsService>();

            services.AddTransient<ICustomersRepository, CustomersRepository>();
            services.AddTransient<ICustomersService, CustomersService>();

            services.AddTransient<IOrderHeaderRepository, OrderHeaderRepository>();
            services.AddTransient<IOrderHeadersService, OrderHeadersService>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
