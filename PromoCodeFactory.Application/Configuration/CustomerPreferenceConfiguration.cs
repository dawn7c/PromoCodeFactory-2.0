using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromoCodeFactory.Domain.Models.PromoCode_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodeFactory.Application.Configuration
{
    public class CustomerPreferenceConfiguration : IEntityTypeConfiguration<CustomerPreference>
    {
        public void Configure(EntityTypeBuilder<CustomerPreference> builder)
        {
            builder
                .ToTable("CustomerPreferences")
                .HasKey(x => new { x.CustomerId, x.PreferenceId });

            builder
                .HasOne(x => x.Customer)
                .WithMany(x => x.Preferences)
                .HasForeignKey(x => x.CustomerId);

            builder
                .HasOne(x => x.Preference)
                .WithMany()
                .HasForeignKey(x => x.PreferenceId);
        }
    }
}
