using Microsoft.EntityFrameworkCore;
using PEACE.api.Models;

namespace PEACE.api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Nutricionista> Nutricionistas { get; set; }
        public DbSet<Anamnese> Anamneses { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Refeicao> Refeicoes { get; set; }
        public DbSet<Alimento> Alimentos { get; set; }
        public DbSet<ItemConsumido> ItensConsumidos { get; set; }
        public DbSet<Suplemento> Suplementos { get; set; }
        public DbSet<LoginAttempt> LoginAttempts { get; set; }
        public DbSet<TabelaAlimentoUSDA> AlimentosUSDA { get; set; }
        public DbSet<NutrienteUSDA> NutrientesUSDA { get; set; }
        public DbSet<TabelaTaco> TabelaTaco { get; set; }
        public DbSet<AcidosGraxosTaco> AcidosGraxosTaco { get; set; }
        public DbSet<AminoacidosTaco> AminoacidosTaco { get; set; }
        public DbSet<AnotacaoRefeicaoHistorico> AnotacoesRefeicaoHistorico { get; set; }
        public DbSet<GastoEnergeticoHistorico> GastoEnergeticoHistorico { get; set; }
        public DbSet<AvaliacaoAntropometrica> AvaliacoesAntropometricas { get; set; }
        public DbSet<PregasCutaneas> PregasCutaneas { get; set; }
        public DbSet<PlanoAlimentar> PlanosAlimentares { get; set; }
        public DbSet<RefeicaoPlano> RefeicoesPlano { get; set; }
        public DbSet<ItemPlanoAlimentar> ItensPlanoAlimentar { get; set; }
        public DbSet<ItemPlano> ItensPlano { get; set; }
        public DbSet<PlanoAlimentar> PlanoAlimentar { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Paciente>()
              .HasOne(p => p.Nutricionista)
              .WithMany(n => n.Pacientes)
              .HasForeignKey(p => p.NutricionistaId)
              .OnDelete(DeleteBehavior.SetNull);
            // Paciente → Refeições (1:N)
            modelBuilder.Entity<Refeicao>()
                .HasOne(r => r.Paciente)
                .WithMany(p => p.Refeicoes)
                .HasForeignKey(r => r.PacienteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AnotacaoRefeicaoHistorico>()
                .HasOne(h => h.Refeicao)
                .WithMany(r => r.HistoricoAnotacoes)
                .HasForeignKey(h => h.RefeicaoId);

            // Refeição → ItensConsumidos (1:N)
            modelBuilder.Entity<ItemConsumido>()
                .HasOne(ic => ic.Refeicao)
                .WithMany(r => r.ItensConsumidos)
                .HasForeignKey(ic => ic.RefeicaoId)
                .OnDelete(DeleteBehavior.Cascade);

            // ItemConsumido → Alimento (N:1)
            modelBuilder.Entity<ItemConsumido>()
                .HasOne(ic => ic.Alimento)
                .WithMany() // Se não houver lista de ItemConsumido no Alimento
                .HasForeignKey(ic => ic.AlimentoId)
                .OnDelete(DeleteBehavior.Restrict);

   

            modelBuilder.Entity<Alimento>()
                .Property(a => a.Descricao)
                .IsRequired();

            modelBuilder.Entity<Refeicao>()
                .Property(r => r.Tipo)
                .IsRequired();

            modelBuilder.Entity<TabelaTaco>()
                .HasOne(t => t.AcidosGraxos)
                .WithOne(a => a.TabelaTaco)
                .HasForeignKey<AcidosGraxosTaco>(a => a.NumeroAlimento);

            modelBuilder.Entity<TabelaTaco>()
                .HasOne(t => t.Aminoacidos)
                .WithOne(a => a.TabelaTaco)
                .HasForeignKey<AminoacidosTaco>(a => a.NumeroAlimento);

            modelBuilder.Entity<AvaliacaoAntropometrica>()
                .HasOne(a => a.PregasCutaneas)
                .WithOne(p => p.AvaliacaoAntropometrica)
                .HasForeignKey<PregasCutaneas>(p => p.AvaliacaoAntropometricaId);

            modelBuilder.Entity<AvaliacaoAntropometrica>()
                .Property(a => a.Sexo)
                .HasConversion<string>();

            modelBuilder.Entity<Anamnese>()
                .Property(a => a.FrequenciaIntestinal)
                .HasConversion<string>();

            modelBuilder.Entity<AvaliacaoAntropometrica>()
                .Property(a => a.FatorAtividade)
                .HasConversion<string>();

            modelBuilder.Entity<GastoEnergeticoHistorico>()
                .Property(g => g.Protocolo)
                .HasConversion<string>();

            modelBuilder.Entity<GastoEnergeticoHistorico>()
                .Property(g => g.Sexo)
                .HasConversion<string>();
        }
    }
}

