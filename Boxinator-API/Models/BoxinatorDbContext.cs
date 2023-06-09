﻿using Microsoft.EntityFrameworkCore;

namespace Boxinator_API.Models
{
    public class BoxinatorDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<User> Users { get; set; }

        public BoxinatorDbContext(DbContextOptions<BoxinatorDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData
            (
                new Country { Id = 1, Name = "Sweden", Multiplier = 0 },
                new Country { Id = 2, Name = "Norway", Multiplier = 0 },
                new Country { Id = 3, Name = "Denmark", Multiplier = 0 },
                new Country { Id = 4, Name = "Finland", Multiplier = 0 },
                new Country { Id = 5, Name = "Estonia", Multiplier = 3 },
                new Country { Id = 6, Name = "Latvia", Multiplier = 3 },
                new Country { Id = 7, Name = "Lithuania", Multiplier = 3 },
                new Country { Id = 8, Name = "Germany", Multiplier = 5 },
                new Country { Id = 9, Name = "Poland", Multiplier = 5 },
                new Country { Id = 10, Name = "Netherlands", Multiplier = 6 },
                new Country { Id = 11, Name = "Belgium", Multiplier = 6 },
                new Country { Id = 12, Name = "Luxembourg", Multiplier = 6 },
                new Country { Id = 13, Name = "United Kingdom", Multiplier = 8 },
                new Country { Id = 14, Name = "Ireland", Multiplier = 8 },
                new Country { Id = 15, Name = "France", Multiplier = 7 },
                new Country { Id = 16, Name = "Spain", Multiplier = 9 },
                new Country { Id = 17, Name = "Portugal", Multiplier = 10 },
                new Country { Id = 18, Name = "Czech Republic", Multiplier = 6 },
                new Country { Id = 19, Name = "Austria", Multiplier = 7 },
                new Country { Id = 20, Name = "Switzerland", Multiplier = 7 },
                new Country { Id = 21, Name = "Italy", Multiplier = 9 },
                new Country { Id = 22, Name = "Iceland", Multiplier = 12 }
            );

            modelBuilder.Entity<Status>().HasData(
            new Status { Id = 1, Name = "CREATED" },
            new Status { Id = 2, Name = "RECEIVED" },
            new Status { Id = 3, Name = "INTRANSIT" },
            new Status { Id = 4, Name = "COMPLETED" },
            new Status { Id = 5, Name = "CANCELLED" }
            ); modelBuilder.Entity<User>().HasData(
            new User { Sub = "44feb5ab-e680-4979-95f6-9cbc18d32077" },
            new User { Sub = "c7643ce3-acaa-470e-8f11-a634dccad52a" },
            new User { Sub = "e7359cd5-6dec-4f8b-be74-0e3148eaa51f" },
            new User { Sub = "bcc36e9d-c309-4248-b777-0421c370eaba" },
            new User { Sub = "9e305eb4-7639-422d-9432-a3e001c6c5b7" }
            );
            modelBuilder.Entity<Shipment>().HasData(
            new Shipment { Id = 1, ReciverName = "John Smith", Weight = 5, BoxColor = "#eb4034", DestinationID = 1, UserSub = "44feb5ab-e680-4979-95f6-9cbc18d32077", Price = 200 },
            new Shipment { Id = 2, ReciverName = "Alice Johnson", Weight = 1, BoxColor = "#15ad07", DestinationID = 14, UserSub = "44feb5ab-e680-4979-95f6-9cbc18d32077", Price = 212 },
            new Shipment { Id = 3, ReciverName = "Bob Thompson", Weight = 1, BoxColor = "#0c1fed", DestinationID = 10, UserSub = "44feb5ab-e680-4979-95f6-9cbc18d32077", Price = 208 },
            new Shipment { Id = 4, ReciverName = "John Smith", Weight = 2, BoxColor = "#fcef2d", DestinationID = 3, UserSub = "44feb5ab-e680-4979-95f6-9cbc18d32077", Price = 200 },
            new Shipment { Id = 5, ReciverName = "Emily Davis", Weight = 1, BoxColor = "#8e2dfc", DestinationID = 19, UserSub = "9e305eb4-7639-422d-9432-a3e001c6c5b7", Price = 208 },
            new Shipment { Id = 6, ReciverName = "Bob Thompson", Weight = 5, BoxColor = "#fa05b9", DestinationID = 17, UserSub = "c7643ce3-acaa-470e-8f11-a634dccad52a", Price = 235 },
            new Shipment { Id = 7, ReciverName = "Emily Davis", Weight = 2, BoxColor = "#f76714", DestinationID = 8, UserSub = "9e305eb4-7639-422d-9432-a3e001c6c5b7", Price = 216 },
            new Shipment { Id = 8, ReciverName = "Bob Davis", Weight = 8, BoxColor = "#1ccfd9", DestinationID = 22, UserSub = "c7643ce3-acaa-470e-8f11-a634dccad52a", Price = 298 }
            );
            modelBuilder.Entity<Shipment>()
               .HasMany(p => p.StatusList)
               .WithMany(m => m.ShipmentsList)
               .UsingEntity<Dictionary<string, object>>(
                   "ShipmentStatus",
                   r => r.HasOne<Status>().WithMany().HasForeignKey("StatusListId"),
                   l => l.HasOne<Shipment>().WithMany().HasForeignKey("ShipmentsListId"),
                   je =>
                   {
                       je.HasKey("StatusListId", "ShipmentsListId");
                       je.HasData(
                           new { StatusListId = 1, ShipmentsListId = 1 },
                           new { StatusListId = 3, ShipmentsListId = 1 },
                           new { StatusListId = 1, ShipmentsListId = 2 },
                           new { StatusListId = 1, ShipmentsListId = 3 },
                           new { StatusListId = 1, ShipmentsListId = 4 },
                           new { StatusListId = 1, ShipmentsListId = 5 },
                           new { StatusListId = 1, ShipmentsListId = 6 },
                           new { StatusListId = 4, ShipmentsListId = 7 },
                           new { StatusListId = 5, ShipmentsListId = 8 }

                       );
                   });
        }
    }
}
   
