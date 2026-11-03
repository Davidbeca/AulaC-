using Microsoft.EntityFrameworkCore;
using TechRepair.Models;

namespace TechRepair.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Equipamento> Equipamentos { get; set; }
    public DbSet<Tecnico> Tecnicos { get; set; }
    public DbSet<Reparacao> Reparacoes { get; set; }
    public DbSet<Intervencao> Intervencoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Cliente
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(200);
            entity.Property(e => e.NIF).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Telefone).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Morada).IsRequired().HasMaxLength(300);
            entity.Property(e => e.CodigoPostal).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Localidade).IsRequired().HasMaxLength(100);
            entity.Property(e => e.DataRegisto).IsRequired();
            entity.HasIndex(e => e.NIF).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
        });

        // Equipamento
        modelBuilder.Entity<Equipamento>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TipoEquipamento).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Marca).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Modelo).IsRequired().HasMaxLength(100);
            entity.Property(e => e.NumeroSerie).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Descricao).HasMaxLength(500);
            entity.Property(e => e.DataRegisto).IsRequired();
            entity.HasIndex(e => e.NumeroSerie).IsUnique();

            entity.HasOne(e => e.Cliente)
                .WithMany(c => c.Equipamentos)
                .HasForeignKey(e => e.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Tecnico
        modelBuilder.Entity<Tecnico>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Telefone).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Especialidade).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Ativo).IsRequired();
            entity.HasIndex(e => e.Email).IsUnique();
        });

        // Reparacao
        modelBuilder.Entity<Reparacao>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DataEntrada).IsRequired();
            entity.Property(e => e.DescricaoAvaria).IsRequired().HasMaxLength(1000);
            entity.Property(e => e.Diagnostico).HasMaxLength(1000);
            entity.Property(e => e.Estado).IsRequired().HasConversion<int>();
            entity.Property(e => e.DataConclusao);
            entity.Property(e => e.DataEntrega);
            entity.Property(e => e.Observacoes).HasMaxLength(1000);

            entity.HasOne(e => e.Equipamento)
                .WithMany(eq => eq.Reparacoes)
                .HasForeignKey(e => e.EquipamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Tecnico)
                .WithMany(t => t.Reparacoes)
                .HasForeignKey(e => e.TecnicoId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Intervencao
        modelBuilder.Entity<Intervencao>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DataIntervencao).IsRequired();
            entity.Property(e => e.DescricaoTrabalho).IsRequired().HasMaxLength(1000);
            entity.Property(e => e.PecasUtilizadas).HasMaxLength(1000);
            entity.Property(e => e.TempoTrabalho).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(e => e.CustoMaoObra).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(e => e.CustoPecas).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(e => e.Observacoes).HasMaxLength(1000);

            entity.HasOne(e => e.Reparacao)
                .WithMany(r => r.Intervencoes)
                .HasForeignKey(e => e.ReparacaoId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}