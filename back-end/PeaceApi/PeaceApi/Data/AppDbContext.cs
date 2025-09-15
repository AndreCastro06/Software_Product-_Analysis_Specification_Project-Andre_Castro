using Microsoft.EntityFrameworkCore;
using PeaceApi.Models;

namespace PeaceApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Nutricionista> Nutricionistas { get; set; }
       
        public DbSet<Paciente> Pacientes { get; set; }

        public DbSet<LoginAttempt> LoginAttempts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            base.OnModelCreating(modelBuilder);




            modelBuilder.Entity<Paciente>()
              .HasOne(p => p.Nutricionista)
              .WithMany(n => n.Pacientes)
              .HasForeignKey(p => p.NutricionistaId);
              
       
    
            // Regras adicionais (opcional)
            modelBuilder.Entity<Paciente>()
                .Property(p => p.Email)
                .IsRequired();


        }
    }
}

