using FlightSchedule.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace FlightSchedule.API.DB
{
    [ExcludeFromCodeCoverage]
    public partial class AlaskaPoCDBContext : DbContext
    {
        public AlaskaPoCDBContext()
        {
        }

        public AlaskaPoCDBContext(DbContextOptions<AlaskaPoCDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FltSeg> FltSeg { get; set; }
        public virtual DbSet<Linops> Linops { get; set; }
        public virtual DbSet<MainStation> MainStation { get; set; }
        public virtual DbSet<WoEngineeringOrder> WoEngineeringOrder { get; set; }
        public virtual DbSet<WorkOrder> WorkOrder { get; set; }
        public virtual DbSet<WoTaskCard> WoTaskCard { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=alaskadb.database.windows.net;Database=AlaskaPoCDB;User Id=sysadmin;password=Password-1");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FltSeg>(entity =>
            {
                entity.ToTable("FltSeg", "FlightSchedule");

                entity.Property(e => e.Aircraftreg)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Airline)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Destination)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.FlightNumber)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.LocalDate).HasColumnType("date");

                entity.Property(e => e.OpsType)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Origin)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Linops>(entity =>
            {
                entity.ToTable("Linops", "MaintenancePlan");

                entity.Property(e => e.LinopsId).HasColumnName("Linops_Id");

                entity.Property(e => e.AircraftReg)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Airline)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.FlightDate).HasColumnType("date");

                entity.Property(e => e.FlightNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaintDate).HasColumnType("date");

                entity.Property(e => e.MaintenanceStatus)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.StationShortCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.WorkControlNum)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MainStation>(entity =>
            {
                entity.HasKey(e => e.StationId);

                entity.ToTable("MainStation", "Station");

                entity.Property(e => e.Airline)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.StationLongCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StationShortCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WoEngineeringOrder>(entity =>
            {
                entity.HasKey(e => new { e.AircraftReg, e.Wonumber, e.Eonumber });

                entity.ToTable("WO_Engineering_Order", "WorkOrder");

                entity.Property(e => e.AircraftReg)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Wonumber).HasColumnName("WONumber");

                entity.Property(e => e.Eonumber)
                    .HasColumnName("EONumber")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WorkOrder>(entity =>
            {
                entity.HasKey(e => e.Wonumber);

                entity.ToTable("WorkOrder", "WorkOrder");

                entity.Property(e => e.Wonumber)
                    .HasColumnName("WONumber")
                    .ValueGeneratedNever();

                entity.Property(e => e.AircraftReg)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ScheduleStartDate).HasColumnType("date");

                entity.Property(e => e.Station)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Wodescription)
                    .IsRequired()
                    .HasColumnName("WODescription")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Wostatus)
                    .IsRequired()
                    .HasColumnName("WOStatus")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WoTaskCard>(entity =>
            {
                entity.HasKey(e => e.TaskCard);

                entity.ToTable("WO_Task_Card", "WorkOrder");

                entity.Property(e => e.TaskCard)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.EngineeringOrder)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TaskCardDescription)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Wonumber).HasColumnName("WONumber");
            });
        }
    }
}
