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
    public class PromoCodeConfiguration : IEntityTypeConfiguration<PromoCode>
    {
        public void Configure(EntityTypeBuilder<PromoCode> builder)
        {
            builder
                .ToTable("PromoCodes")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.ServiceInfo)
                .IsRequired()
                .HasMaxLength(200);

            builder
                .Property(x => x.BeginDate)
                .IsRequired();

            builder
                .Property(x => x.EndDate)
                .IsRequired();


            builder
                .HasOne(x => x.Preference)
                .WithMany();
                
        }
    }
}
