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
    public class PartnerConfiguration : IEntityTypeConfiguration<Partner>
    {
        public void Configure(EntityTypeBuilder<Partner> builder)
        {
            builder
                .ToTable("Partners")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.PartnerName)
                .IsRequired()
                .HasMaxLength(15);

            builder
                .Property(x => x.PartnerManager)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(x => x.PartnerManagerId)
                .IsRequired()
                .HasMaxLength(100);
            //builder
            //    .HasOne(x => x.PromoCodes)
            //    .WithMany()
            //    .HasForeignKey(x => x.PromoCodeId);

        }
    }
}
