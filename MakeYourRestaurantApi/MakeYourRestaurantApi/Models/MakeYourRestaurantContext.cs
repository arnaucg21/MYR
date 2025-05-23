using System;
using Microsoft.EntityFrameworkCore;

namespace MakeYourRestaurantApiV1.Models
{
    public partial class MakeYourRestaurantContext : DbContext
    {
        public MakeYourRestaurantContext() { }

        public MakeYourRestaurantContext(DbContextOptions<MakeYourRestaurantContext> options)
            : base(options) { }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Meal> Meals { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<OrderList> OrderLists { get; set; } = null!;
        public virtual DbSet<Price> Prices { get; set; } = null!;
        public virtual DbSet<Qrcode> Qrcodes { get; set; } = null!;
        public virtual DbSet<Restaurant> Restaurants { get; set; } = null!;
        public virtual DbSet<RestaurantTable> RestaurantTables { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Name).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Photo).HasMaxLength(200).IsUnicode(false);

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK__Category__Restau__34C8D9D1");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Username).HasMaxLength(25).IsUnicode(false);
                entity.Property(e => e.Password).HasMaxLength(25).IsUnicode(false);

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK__Employees__Resta__267ABA7A");
            });

            modelBuilder.Entity<Meal>(entity =>
            {
                entity.ToTable("Meal");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Name).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Details).HasMaxLength(150).IsUnicode(false);
                entity.Property(e => e.Photo).HasMaxLength(200).IsUnicode(false);
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Meals)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Meal__Time__37A5467C");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Meals)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK__Meal__Restaurant__3A81B327");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Tickets");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK__Orders__Restaura__31EC6D26");
            });

            modelBuilder.Entity<OrderList>(entity =>
            {
                entity.ToTable("OrderList");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.TicketId).HasColumnName("ticketId");
                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");
                entity.Property(e => e.Done).HasColumnName("Done").HasDefaultValue(false);

                entity.HasOne(d => d.Meal)
                    .WithMany(p => p.OrderLists)
                    .HasForeignKey(d => d.MealId)
                    .HasConstraintName("FK__OrderList__MealI__3F466844");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.OrderLists)
                    .HasForeignKey(d => d.TicketId)
                    .HasConstraintName("FK__OrderList__ticket__3E52440B");
            });

            modelBuilder.Entity<Price>(entity =>
            {
                entity.ToTable("Price");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Value).HasMaxLength(10).IsUnicode(false);

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Prices)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK__Price__Value__2C3393D0");
            });

            modelBuilder.Entity<Qrcode>(entity =>
            {
                entity.ToTable("QRCodes");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Photo).HasMaxLength(200).IsUnicode(false);

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Qrcodes)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK__QRCodes__Restaur__44FF419A");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("Restaurant");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Name).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Address).HasMaxLength(100).IsUnicode(false);
                entity.Property(e => e.Phone).HasMaxLength(20).IsUnicode(false);
            });

            modelBuilder.Entity<RestaurantTable>(entity =>
            {
                entity.ToTable("RestaurantTable");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.TableNumber).HasColumnName("TableNumber");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.RestaurantTables)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK__RestaurantTable__Restaurant");

                entity.HasOne(d => d.Qrcode)
                    .WithMany(p => p.RestaurantTables)
                    .HasForeignKey(d => d.QrcodeId)
                    .HasConstraintName("FK__RestaurantTable__Qrcode");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
