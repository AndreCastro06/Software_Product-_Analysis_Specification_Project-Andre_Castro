using Microsoft.EntityFrameworkCore;
using PeaceAPI.Models;

namespace PeaceAPI.Data;

public class PeaceDbContext : DbContext
{
    public PeaceDbContext(DbContextOptions<PeaceDbContext> options) : base(options) { }

    public DbSet<Nutricionista> Nutricionistas => Set<Nutricionista>();
    public DbSet<Paciente> Pacientes => Set<Paciente>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Nutricionista>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.NomeCompleto).HasMaxLength(150).IsRequired();
            e.Property(x => x.Email).HasMaxLength(150).IsRequired();
            e.HasIndex(x => x.Email).IsUnique();
            e.Property(x => x.Role).HasMaxLength(30).IsRequired();
        });

        modelBuilder.Entity<Paciente>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.NomeCompleto).HasMaxLength(150).IsRequired();
            e.HasOne<Nutricionista>()
                .WithMany()
                .HasForeignKey(x => x.NutricionistaId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}