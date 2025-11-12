using Microsoft.EntityFrameworkCore;
using sistem_pengelolaan_lab.Models; // Pastikan ini ada

namespace sistem_pengelolaan_lab.Models // Ganti dengan namespace DbContext Anda
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Ruangan> Ruangan { get; set; }
        public DbSet<Storage> Storage { get; set; } // Jika Anda memiliki DbSet untuk Storage

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Secara eksplisit definisikan Primary Key untuk Ruangan
            modelBuilder.Entity<Ruangan>()
                .HasKey(r => r.ID_Ruangan); // <-- Tambahkan baris ini

            // Secara eksplisit definisikan Primary Key untuk Storage (jika ada)
            modelBuilder.Entity<Storage>()
                .HasKey(s => s.ID_Storage); // <-- Tambahkan baris ini

            // Konfigurasi relasi (jika belum otomatis)
            modelBuilder.Entity<Storage>()
                .HasOne(s => s.Ruangan)
                .WithMany(r => r.Storages)
                .HasForeignKey(s => s.ID_Ruangan)
                .OnDelete(DeleteBehavior.Restrict); // Atau DeleteBehavior.Cascade, sesuai kebutuhan
        }
    }
}