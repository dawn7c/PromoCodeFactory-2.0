using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Security.Cryptography.Xml;

namespace PromoCodeFactory.Infrastructure
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<IRepository<Employee>, EfRepository<Employee>>();
            services.AddScoped<IRepository<Role>, EfRepository<Role>>();
            services.AddScoped<IRepository<Preference>, EfRepository<Preference>>();
            services.AddScoped<IRepository<Customer>, EfRepository<Customer>>();
            services.AddScoped<IRepository<Promocode>, EfRepository<Promocode>>();
            services.AddAutoMapper(typeof(Startup));


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
