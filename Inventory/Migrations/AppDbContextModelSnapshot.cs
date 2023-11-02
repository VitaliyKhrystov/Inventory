﻿// <auto-generated />
using System;
using Inventory.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Inventory.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Inventory.Domain.Entities.CategoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Inventory.Domain.Entities.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("ImageData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("VariantEntityId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("VariantEntityId")
                        .IsUnique();

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Inventory.Domain.Entities.ProductEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CategoryEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Rate")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryEntityId")
                        .IsUnique()
                        .HasFilter("[CategoryEntityId] IS NOT NULL");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Inventory.Domain.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Hiten",
                            Password = "Hiten",
                            Role = "User"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Admin",
                            Password = "admin",
                            Role = "Admin"
                        });
                });

            modelBuilder.Entity("Inventory.Domain.Entities.VariantEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProductEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("StockInHand")
                        .HasColumnType("int");

                    b.Property<string>("VariantId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductEntityId");

                    b.ToTable("Variants");
                });

            modelBuilder.Entity("Inventory.Domain.Entities.Image", b =>
                {
                    b.HasOne("Inventory.Domain.Entities.VariantEntity", "Variant")
                        .WithOne("Image")
                        .HasForeignKey("Inventory.Domain.Entities.Image", "VariantEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Variant");
                });

            modelBuilder.Entity("Inventory.Domain.Entities.ProductEntity", b =>
                {
                    b.HasOne("Inventory.Domain.Entities.CategoryEntity", "Category")
                        .WithOne("Product")
                        .HasForeignKey("Inventory.Domain.Entities.ProductEntity", "CategoryEntityId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Inventory.Domain.Entities.VariantEntity", b =>
                {
                    b.HasOne("Inventory.Domain.Entities.ProductEntity", "Product")
                        .WithMany("Variants")
                        .HasForeignKey("ProductEntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Inventory.Domain.Entities.CategoryEntity", b =>
                {
                    b.Navigation("Product");
                });

            modelBuilder.Entity("Inventory.Domain.Entities.ProductEntity", b =>
                {
                    b.Navigation("Variants");
                });

            modelBuilder.Entity("Inventory.Domain.Entities.VariantEntity", b =>
                {
                    b.Navigation("Image");
                });
#pragma warning restore 612, 618
        }
    }
}
