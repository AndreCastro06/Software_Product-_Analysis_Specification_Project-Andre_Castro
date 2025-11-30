using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

        // Tabelas USDA e TACO
        public DbSet<TabelaAlimentoUSDA> AlimentosUSDA { get; set; }
        public DbSet<NutrienteUSDA> NutrientesUSDA { get; set; }
        public DbSet<TabelaTaco> TabelaTaco { get; set; }
        public DbSet<AcidosGraxosTaco> AcidosGraxosTaco { get; set; }
        public DbSet<AminoacidosTaco> AminoacidosTaco { get; set; }

        // Histórico de Anotações
        public DbSet<AnotacaoRefeicaoHistorico> AnotacoesRefeicaoHistorico { get; set; }

        // Gasto energético
        public DbSet<GastoEnergeticoHistorico> GastoEnergeticoHistorico { get; set; }

        // Avaliação física
        public DbSet<AvaliacaoAntropometrica> AvaliacoesAntropometricas { get; set; }
        public DbSet<PregasCutaneas> PregasCutaneas { get; set; }

        // Plano alimentar 
        public DbSet<PlanoAlimentar> PlanosAlimentares { get; set; }
        public DbSet<RefeicaoPlano> RefeicoesPlano { get; set; }
        public DbSet<ItemPlanoAlimentar> ItensPlanoAlimentar { get; set; }
        public DbSet<ItemPlano> ItensPlano { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ========================
            //     CONVERSÃO DATETIME → UTC
            // ========================
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime))
                    {
                        property.SetValueConverter(new ValueConverter<DateTime, DateTime>(
                            v => v.ToUniversalTime(),
                            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                    }
                }
            }

            // ========================
            //     RELACIONAMENTOS
            // ========================

            // Paciente ←→ Nutricionista
            modelBuilder.Entity<Paciente>()
                .HasOne(p => p.Nutricionista)
                .WithMany(n => n.Pacientes)
                .HasForeignKey(p => p.NutricionistaId)
                .OnDelete(DeleteBehavior.SetNull);

            // Paciente ←→ Refeições (1:N)
            modelBuilder.Entity<Refeicao>()
                .HasOne(r => r.Paciente)
                .WithMany(p => p.Refeicoes)
                .HasForeignKey(r => r.PacienteId)
                .OnDelete(DeleteBehavior.Cascade);

            // Refeição ←→ Histórico de anotações (1:N)
            modelBuilder.Entity<AnotacaoRefeicaoHistorico>()
                .HasOne(h => h.Refeicao)
                .WithMany(r => r.HistoricoAnotacoes)
                .HasForeignKey(h => h.RefeicaoId);

            // Refeição ←→ Itens Consumidos (1:N)
            modelBuilder.Entity<ItemConsumido>()
                .HasOne(ic => ic.Refeicao)
                .WithMany(r => r.ItensConsumidos)
                .HasForeignKey(ic => ic.RefeicaoId)
                .OnDelete(DeleteBehavior.Cascade);

            // ItemConsumido ←→ Alimento (N:1)
            modelBuilder.Entity<ItemConsumido>()
                .HasOne(ic => ic.Alimento)
                .WithMany()
                .HasForeignKey(ic => ic.AlimentoId)
                .OnDelete(DeleteBehavior.Restrict);

            // TACO: 1:1 TabelaTaco ←→ Ácidos Graxos
            modelBuilder.Entity<TabelaTaco>()
                .HasOne(t => t.AcidosGraxos)
                .WithOne(a => a.TabelaTaco)
                .HasForeignKey<AcidosGraxosTaco>(a => a.NumeroAlimento);

            // TACO: 1:1 TabelaTaco ←→ Aminoácidos
            modelBuilder.Entity<TabelaTaco>()
                .HasOne(t => t.Aminoacidos)
                .WithOne(a => a.TabelaTaco)
                .HasForeignKey<AminoacidosTaco>(a => a.NumeroAlimento);

            // Avaliação ←→ Pregas Cutâneas (1:1)
            modelBuilder.Entity<AvaliacaoAntropometrica>()
                .HasOne(a => a.PregasCutaneas)
                .WithOne(p => p.AvaliacaoAntropometrica)
                .HasForeignKey<PregasCutaneas>(p => p.AvaliacaoAntropometricaId);

            // ========================
            //   CONVERSÕES ENUM → STRING
            // ========================

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
