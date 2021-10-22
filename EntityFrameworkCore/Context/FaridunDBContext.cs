using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.Models;

namespace EntityFrameworkCore.Context
{
    public partial class FaridunDBContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data source=localhost; Initial catalog=MSSQL; Integrated security=true;");
            }
        }
        public virtual DbSet<Customers> Customers { get; set; }
        public FaridunDBContext()
        {
        }

        public FaridunDBContext(DbContextOptions<FaridunDBContext> options)
            : base(options)
        {
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>(entity =>
            {
                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}