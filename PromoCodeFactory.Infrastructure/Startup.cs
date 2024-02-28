﻿using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PromoCodeFactory.Application.DatabaseContext;
using PromoCodeFactory.Application.Repositories;
using PromoCodeFactory.Domain.Abstractions;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace PromoCodeFactory.Infrastructure
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PromoCode Factory API Doc", Version = "1.0" });
            });
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString));

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    app.UseHsts();
                }



                app.UseSwagger();
                app.UseSwaggerUI(x =>
                {
                    x.SwaggerEndpoint("/swagger/v1/swagger.json", "PromoCode Factory API Doc V1");
                    x.DefaultModelsExpandDepth(-1);
                    x.DocExpansion(DocExpansion.List);
                });

                app.UseHttpsRedirection();

                app.UseRouting();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            
        }
    }
}
