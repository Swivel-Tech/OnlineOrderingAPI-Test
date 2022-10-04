using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OnlineOrdering.Data.DatabaseContext
{
    public partial class OnlineOrderingDBContext : DbContext
    {
        public OnlineOrderingDBContext(DbContextOptions<OnlineOrderingDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<OrderHeader> OrderHeaders { get; set; } = null!;
        public virtual DbSet<OrderLineItem> OrderLineItems { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.HasIndex(e => new { e.Email, e.Phone }, "UK_Customer_Email_Phone")
                    .IsUnique();

                entity.Property(e => e.Email).HasMaxLength(390);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<OrderHeader>(entity =>
            {
                entity.ToTable("OrderHeader");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("(N'PENDING')");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.OrderHeaders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Customer");
            });

            modelBuilder.Entity<OrderLineItem>(entity =>
            {
                entity.ToTable("OrderLineItem");

                entity.Property(e => e.LineAmount).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.OrderHeader)
                    .WithMany(p => p.OrderLineItems)
                    .HasForeignKey(d => d.OrderHeaderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderLineItem_OrderHeader");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderLineItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderLineItem_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
