using FluentValidation;
using FunBooksAndVideos.Infrastructure.Repository;
using FunBooksAndVideos.Service.Customers.DTO;
using FunBooksAndVideos.Service.PipelineBehaviours;
using FunBooksAndVideos.Service.Resources;
using FunBooksAndVideos.Service.Validators;
using FunBooksAndVideos.WebApi.Infrastructure.Database;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NLog;
using System;
using System.IO;

namespace FunBooksAndVideos.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)));

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(string.Format(@"{0}\EcomShop.WebApi.xml", System.AppDomain.CurrentDomain.BaseDirectory));
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "e-commerce Shop API",
                });
            });
            #endregion Swagger
            services.AddScoped<IDatabaseContext>(provider => provider.GetService<DatabaseContext>());
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers();
            services.AddScoped<IValidator<CustomerRequest>, CreateCustomerValidator>();
            services.AddScoped<IValidator<MembershipRequest>, CreateMembershipValidator>();
            services.AddScoped<IValidator<OrderRequest>, CreateOrderValidator>();
            services.AddScoped<IValidator<ProductRequest>, CreateProductValidator>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddSingleton<FunBooksAndVideos.Infrastructure.Logging.ILogger, FunBooksAndVideos.Infrastructure.Logging.Logger>();
            services.AddAutoMapper(typeof(CustomerAutoMapperProfile));
            services.AddMemoryCache();
            services.AddResponseCompression();
            GlobalDiagnosticsContext.Set("AppDirectory", @"C:\Temp\");
            NLog.Common.InternalLogger.LogFile = @"C:\Temp\nlog-internal.log";
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseResponseCompression();
            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FunBooksAndVideos.WebApi");
            });
            #endregion Swagger
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}