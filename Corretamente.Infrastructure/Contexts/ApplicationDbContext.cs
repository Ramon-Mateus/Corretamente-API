using Microsoft.EntityFrameworkCore;
using Corretamente.Domain.Entities;

namespace Corretamente.Infrastructure.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Imovel> Imoveis { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Email)
                .HasMaxLength(100);

            entity.Property(e => e.Telefone)
                .HasMaxLength(20);

            entity.Property(e => e.Documento)
                .IsRequired()
                .HasMaxLength(18);

            entity.HasIndex(e => e.Documento)
                .IsUnique();

            entity.HasIndex(e => e.Email)
                .IsUnique()
                .HasFilter("[Email] IS NOT NULL");
        });

        modelBuilder.Entity<Imovel>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Valor)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            entity.Property(e => e.Logradouro)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Cidade)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(2);

            entity.Property(e => e.Cep)
                .IsRequired()
                .HasMaxLength(9);

            entity.HasOne(e => e.Locatario)
                .WithMany(e => e.Imoveis)
                .HasForeignKey(e => e.LocatarioId)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }


}
