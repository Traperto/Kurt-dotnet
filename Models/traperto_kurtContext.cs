using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ColaTerminal.Models
{
    public partial class traperto_kurtContext : DbContext
    {
        public traperto_kurtContext()
        {
        }

        public traperto_kurtContext(DbContextOptions<traperto_kurtContext> options)
            : base(options)
        {

        }

        public DbSet<BalanceTransaction> BalanceTransaction { get; set; }
        public DbSet<Drink> Drink { get; set; }
        public DbSet<Proceed> Proceed { get; set; }
        public DbSet<Refill> Refill { get; set; }
        public DbSet<RefillContainment> RefillContainment { get; set; }
        public DbSet<User> User { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<BalanceTransaction>(entity =>
            {
                entity.ToTable("balanceTransaction");

                entity.HasIndex(e => e.UserId)
                    .HasName("balanceTransaction_user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BalanceTransaction)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("balanceTransaction_user");
            });

            modelBuilder.Entity<Drink>(entity =>
            {
                entity.ToTable("drink");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasColumnType("int(2)");
            });

            modelBuilder.Entity<Proceed>(entity =>
            {
                entity.ToTable("proceed");

                entity.HasIndex(e => e.DrinkId)
                    .HasName("proceed_drink");

                entity.HasIndex(e => e.UserId)
                    .HasName("proceed_user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DrinkId).HasColumnName("drinkId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Drink)
                    .WithMany(p => p.Proceed)
                    .HasForeignKey(d => d.DrinkId)
                    .HasConstraintName("proceed_drink");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Proceed)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("proceed_user");
            });

            modelBuilder.Entity<Refill>(entity =>
            {
                entity.ToTable("refill");

                entity.HasIndex(e => e.UserId)
                    .HasName("refill_user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Refill)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("refill_user");
            });

            modelBuilder.Entity<RefillContainment>(entity =>
            {
                entity.HasKey(e => new { e.DrinkId, e.RefillId })
                    .HasName("PRIMARY");

                entity.ToTable("refillContainment");

                entity.HasIndex(e => e.RefillId)
                    .HasName("conainment_refill");

                entity.Property(e => e.DrinkId).HasColumnName("drinkId");

                entity.Property(e => e.RefillId).HasColumnName("refillId");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Drink)
                    .WithMany(p => p.RefillContainment)
                    .HasForeignKey(d => d.DrinkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("containment_drink");

                entity.HasOne(d => d.Refill)
                    .WithMany(p => p.RefillContainment)
                    .HasForeignKey(d => d.RefillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("conainment_refill");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.FirstName)
                    .HasColumnName("firstName")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.LastName)
                    .HasColumnName("lastName")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.RfId)
                    .HasColumnName("rfId")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.UserName)
                    .HasColumnName("userName")
                    .HasColumnType("varchar(50)");
            });
        }
    }
}
