using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Receiver> Receivers { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipmentStatus> ShipmentStatuses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Pilot> Pilots { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<ShipmentCost> ShipmentCosts { get; set; }
        public DbSet<ShipmentConfirmation> ShipmentConfirmations { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<ShipmentReturn> ShipmentReturns { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<AdminArea> AdminAreas { get; set; }
        public DbSet<CompanyClient> CompanyClients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ShipmentCost>()
                .Property(x => x.Cost)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ShipmentCost>()
                .Property(x => x.Discount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ShipmentCost>()
                .Property(x => x.TotalCost)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.DistrictDelivery)
                .WithMany()
                .HasForeignKey(s => s.DistrictDeliveryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.Receiver)
                .WithMany()
                .HasForeignKey(s => s.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
