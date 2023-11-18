using CarService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CarService.DataAccess;

public class CarServiceDbContext : DbContext
{
    public DbSet<BrandEntity> Brands { get; set; }
    public DbSet<CarEntity> Cars { get; set; }
    public DbSet<ColorEntity> Colors { get; set; }
    public DbSet<FeedbackEntity> Feedbacks { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<ServiceEntity> Services { get; set; }
    public DbSet<UserEntity> Users { get; set; }


    public CarServiceDbContext(DbContextOptions options) : base(options)    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<UserEntity>().HasIndex(x => x.ExternalId).IsUnique();

        modelBuilder.Entity<ColorEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<ColorEntity>().HasIndex(x => x.ExternalId).IsUnique();

        modelBuilder.Entity<BrandEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<BrandEntity>().HasIndex(x => x.ExternalId).IsUnique();

        modelBuilder.Entity<ServiceEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<ServiceEntity>().HasIndex(x => x.ExternalId).IsUnique();

        modelBuilder.Entity<CarEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<CarEntity>().HasIndex(x => x.ExternalId).IsUnique();
        modelBuilder.Entity<CarEntity>().HasOne(x => x.User)
            .WithMany(x => x.Cars)
            .HasForeignKey(x => x.UserId);
        modelBuilder.Entity<CarEntity>().HasOne(x => x.Color)
            .WithMany(x => x.Cars)
            .HasForeignKey(x => x.ColorId);
        modelBuilder.Entity<CarEntity>().HasOne(x => x.Brand)
            .WithMany(x => x.Cars)
            .HasForeignKey(x => x.BrandId);


        modelBuilder.Entity<OrderEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<OrderEntity>().HasIndex(x => x.ExternalId).IsUnique();
        modelBuilder.Entity<OrderEntity>().HasOne(x => x.Car)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.CarId);
        modelBuilder.Entity<OrderEntity>().HasOne(x => x.Service) 
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.ServiceId);

        modelBuilder.Entity<FeedbackEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<FeedbackEntity>().HasIndex(x => x.ExternalId).IsUnique();
        modelBuilder.Entity<FeedbackEntity>().HasOne(x => x.Order)
            .WithMany(x => x.Feedbacks)
            .HasForeignKey(x => x.OrderId);
    }
}