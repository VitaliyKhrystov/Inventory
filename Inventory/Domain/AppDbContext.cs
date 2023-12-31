﻿using Inventory.Domain.Entities;
using Inventory.Service;
using Microsoft.EntityFrameworkCore;
using System;


namespace Inventory.Domain
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<SupplierEntity> Suppliers { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<VariantEntity> Variants { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<Mobile> Mobiles { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<PurchaseOrderEntity> PurchaseOrders { get; set; }
        public DbSet<PurchaseEntity> Purchases { get; set; }
        public DbSet<PurchaseVariant> PurchaseVariant { get; set; }
        public DbSet<SalesOrderEntity> SalesOrders { get; set; }
        public DbSet<SalesOrderVariantEntity> SalesOrderVariants { get; set; }
        public DbSet<SalesEntity> Sales { get; set; }
        public DbSet<SalesVariantEntity> SalesVariants { get; set; }
        public DbSet<SalesSummaryEntity> SalesSummary { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasData(new List<UserEntity>()
            {
                new UserEntity(){ Id = 1, Name = "Hiten", Password = "J031289", Role = "User"},
                new UserEntity(){ Id = 2, Name = "Admin", Password = "Admin@", Role = "Admin"}
            });

            modelBuilder.Entity<PurchaseOrderEntity>()
                        .Property(p => p.Date)
                        .HasConversion<DateOnlyConverter>();

            modelBuilder.Entity<PurchaseOrderEntity>()
                        .Property(p => p.DueDate)
                        .HasConversion<DateOnlyConverter>();

            modelBuilder.Entity<PurchaseEntity>()
                       .Property(p => p.Date)
                       .HasConversion<DateOnlyConverter>();

            modelBuilder.Entity<SalesOrderEntity>()
                        .Property(p => p.DueDate)
                        .HasConversion<DateOnlyConverter>();

            modelBuilder.Entity<SalesOrderEntity>()
                        .Property(p => p.Date)
                        .HasConversion<DateOnlyConverter>();

            modelBuilder.Entity<SalesEntity>()
                        .Property(p => p.Date)
                        .HasConversion<DateOnlyConverter>();

            modelBuilder.Entity<CategoryEntity>()
                        .HasMany(p => p.Products)
                        .WithOne(p => p.Category);

            modelBuilder.Entity<ProductEntity>()
                        .HasMany(p => p.Variants)
                        .WithOne(v => v.Product)
                        .HasForeignKey(k => k.ProductEntityId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VariantEntity>()
                        .HasOne(v => v.Image)
                        .WithOne(i => i.Variant)
                        .HasForeignKey<Image>(k => k.VariantEntityId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CustomerEntity>()
                        .HasMany(c => c.Mobiles)
                        .WithOne(m => m.Customer)
                        .HasForeignKey(k => k.CustomerEntityId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SupplierEntity>()
                        .HasMany(s => s.Purchases)
                        .WithOne(p => p.Supplier)
                        .HasForeignKey(k => k.SupplierEntityId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SupplierEntity>()
                        .HasMany(c => c.Mobiles)
                        .WithOne(m => m.Supplier)
                        .HasForeignKey(k => k.SupplierEntityId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductEntity>()
                      .HasMany(p => p.PurchaseVariants)
                      .WithOne(v => v.Product)
                      .HasForeignKey(k => k.ProductEntityId)
                      .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VariantEntity>()
                      .HasMany(v => v.PurchaseVariants)
                      .WithOne(p => p.ProductVariant)
                      .HasForeignKey(k => k.VariantEntityId)
                      .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PurchaseEntity>()
                        .HasMany(p => p.PurchaseVariants)
                        .WithOne(v => v.Purchase)
                        .HasForeignKey(k => k.PurchaseEntityId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CustomerEntity>()
                        .HasMany(c => c.SalesOrders)
                        .WithOne(s => s.Customer)
                        .HasForeignKey(k => k.CustomerEntityId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SalesOrderEntity>()
                        .HasMany(s => s.SalesOrderVariants)
                        .WithOne(v => v.SalesOrder)
                        .HasForeignKey(k => k.SalesOrderEntityId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductEntity>()
                        .HasMany(p => p.SalesOrderVariants)
                        .WithOne(v => v.Product)
                        .HasForeignKey(k => k.ProductEntityId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VariantEntity>()
                        .HasMany(v => v.SalesOrderVariants)
                        .WithOne(p => p.ProductVariant)
                        .HasForeignKey(k => k.VariantEntityId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SalesEntity>()
                      .HasMany(s => s.SalesVariants)
                      .WithOne(v => v.Sale)
                      .HasForeignKey(k => k.SalesEntityId)
                      .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SalesEntity>()
                    .HasMany(s => s.SalesSummaries)
                    .WithOne(v => v.Sale)
                    .HasForeignKey(k => k.SalesEntityId)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductEntity>()
                       .HasMany(p => p.SalesVariants)
                       .WithOne(v => v.Product)
                       .HasForeignKey(k => k.ProductEntityId)
                       .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VariantEntity>()
                        .HasMany(v => v.SalesVariants)
                        .WithOne(p => p.ProductVariant)
                        .HasForeignKey(k => k.VariantEntityId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CustomerEntity>()
                        .HasMany(c => c.Sales)
                        .WithOne(s => s.Customer)
                        .HasForeignKey(k => k.CustomerEntityId)
                        .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
