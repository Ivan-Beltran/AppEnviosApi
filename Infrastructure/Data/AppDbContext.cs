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

            // =========================================================================
            // 1. CONFIGURACIÓN TPT (HERENCIA DE ROLES / EXTENSION TABLES)
            // =========================================================================
            // Indicamos a EF Core que el ID de estas tablas hijas NO es autoincrementable (Identity),
            // sino que hereda y depende directamente del Id generado por la tabla de Users.

            modelBuilder.Entity<AdminArea>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Pilot>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<CompanyClient>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Admin>()
                .Property(p => p.Id)
                .ValueGeneratedNever();


            // =========================================================================
            // 2. PROTECCIÓN CONTRA EL BORRADO EN CASCADA EN ENVIOS (SHIPMENTS)
            // =========================================================================
            // Cambiamos todos los DeleteBehavior a "Restrict" (NO ACTION en SQL Server).
            // Esto rompe los bucles y caminos múltiples de borrado que hacían fallar la migración.

            // Corrección del primer error (Receiver)
            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.Receiver)
                .WithMany()
                .HasForeignKey(s => s.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            // Corrección del segundo error (DistrictDelivery)
            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.DistrictDelivery) // Asegúrate de que este sea el nombre de la propiedad en tu clase Shipment
                .WithMany()
                .HasForeignKey(s => s.DistrictDeliveryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Prevención de errores futuros en cascada por Branch (Sucursales)
            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.Branch)
                .WithMany()
                .HasForeignKey(s => s.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            // Prevención por el Usuario que lo creó
            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.CreatedByUser)
                .WithMany()
                .HasForeignKey(s => s.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Prevención por el Estado del Envío
            modelBuilder.Entity<Shipment>()
                .HasOne(s => s.ShipmentStatus)
                .WithMany()
                .HasForeignKey(s => s.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
