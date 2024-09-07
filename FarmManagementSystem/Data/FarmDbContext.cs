using FarmManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FarmManagementSystem.Data
{
    public class FarmDbContext : DbContext
    {
        public FarmDbContext(DbContextOptions<FarmDbContext> options) : base(options) 
        {
        }

        public DbSet<Worker> Workers { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Crop> Crops { get; set; }
        public DbSet<Harvest> Harvests { get; set; }
        public DbSet<WorkerAssignment> WorkerAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkerAssignment>()
                .HasKey(wa => new { wa.WorkerID, wa.FieldID });

            modelBuilder.Entity<WorkerAssignment>()
                .HasOne(wa => wa.Worker)
                .WithMany(w => w.WorkerAssignments)
                .HasForeignKey(wa => wa.WorkerID);

            modelBuilder.Entity<WorkerAssignment>()
                .HasOne(wa => wa.Field)
                .WithMany(f => f.WorkerAssignments)
                .HasForeignKey(wa => wa.FieldID);
        }
    }
}
