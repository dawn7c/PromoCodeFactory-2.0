using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Application.Configuration;
using PromoCodeFactory.Domain.Models.Administration;
using PromoCodeFactory.Domain.Models.PromoCode_Management;


namespace PromoCodeFactory.Application.DatabaseContext
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<Partner> Partners { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options)
                : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new EmployeeConfiguration());
            builder.ApplyConfiguration(new PreferenceConfiguration());
            builder.ApplyConfiguration(new PromoCodeConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
