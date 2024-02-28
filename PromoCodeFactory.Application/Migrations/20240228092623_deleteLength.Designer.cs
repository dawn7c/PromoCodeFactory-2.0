﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PromoCodeFactory.Application.DatabaseContext;

#nullable disable

namespace PromoCodeFactory.Application.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240228092623_deleteLength")]
    partial class deleteLength
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PromoCodeFactory.Domain.Models.Administration.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("PromoCodeFactory.Domain.Models.Administration.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("PromoCodeFactory.Domain.Models.PromoCode_Management.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("PromoCodeFactory.Domain.Models.PromoCode_Management.Partner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PartnerManagerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PartnerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PartnerManagerId");

                    b.ToTable("Partners");
                });

            modelBuilder.Entity("PromoCodeFactory.Domain.Models.PromoCode_Management.Preference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Preferences");
                });

            modelBuilder.Entity("PromoCodeFactory.Domain.Models.PromoCode_Management.PromoCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BeginDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PartnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PreferenceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ServiceInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PartnerId");

                    b.HasIndex("PreferenceId");

                    b.ToTable("PromoCode");
                });

            modelBuilder.Entity("PromoCodeFactory.Domain.Models.Administration.Employee", b =>
                {
                    b.HasOne("PromoCodeFactory.Domain.Models.Administration.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("PromoCodeFactory.Domain.Models.PromoCode_Management.Partner", b =>
                {
                    b.HasOne("PromoCodeFactory.Domain.Models.Administration.Employee", "PartnerManager")
                        .WithMany()
                        .HasForeignKey("PartnerManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PartnerManager");
                });

            modelBuilder.Entity("PromoCodeFactory.Domain.Models.PromoCode_Management.Preference", b =>
                {
                    b.HasOne("PromoCodeFactory.Domain.Models.PromoCode_Management.Customer", null)
                        .WithMany("Preferences")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("PromoCodeFactory.Domain.Models.PromoCode_Management.PromoCode", b =>
                {
                    b.HasOne("PromoCodeFactory.Domain.Models.PromoCode_Management.Customer", "Customer")
                        .WithMany("PromoCodes")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PromoCodeFactory.Domain.Models.PromoCode_Management.Partner", "Partner")
                        .WithMany("PromoCodes")
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PromoCodeFactory.Domain.Models.PromoCode_Management.Preference", "Preference")
                        .WithMany()
                        .HasForeignKey("PreferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Partner");

                    b.Navigation("Preference");
                });

            modelBuilder.Entity("PromoCodeFactory.Domain.Models.PromoCode_Management.Customer", b =>
                {
                    b.Navigation("Preferences");

                    b.Navigation("PromoCodes");
                });

            modelBuilder.Entity("PromoCodeFactory.Domain.Models.PromoCode_Management.Partner", b =>
                {
                    b.Navigation("PromoCodes");
                });
#pragma warning restore 612, 618
        }
    }
}
