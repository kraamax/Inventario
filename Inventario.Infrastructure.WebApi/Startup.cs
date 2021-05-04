using Inventario.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventario.Domain.Contracts;
using Inventario.Infrastructure.Data.Base;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Infrastructure.WebApi
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
            var connectionString = Configuration.GetConnectionString("InventarioContext");//obtiene la configuracion del appsettitgs

            services.AddDbContext<InventarioContext>(opt => opt.UseSqlite(connectionString));
            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson();
            services.AddScoped<IUnitOfWork, UnitOfWork>(); //Crear Instancia por peticion
            services.AddScoped<IProductoRepository, ProductoRepository>(); //Crear Instancia por peticion
            services.AddScoped<IDbContext, InventarioContext>(); //Crear Instancia por peticion
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Inventario.Infrastructure.WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventario.Infrastructure.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
