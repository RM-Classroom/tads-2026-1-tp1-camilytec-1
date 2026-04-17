using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
    : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Fabricante> Fabricantes { get; set; }
    public DbSet<Veiculo> Veiculos { get; set; }
    public DbSet<Aluguel> Alugueis { get; set; }
    public DbSet<Pagamento> Pagamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluguel>()
            .Property(a => a.ValorDiaria)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Aluguel>()
            .Property(a => a.ValorTotal)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Pagamento>()
            .Property(p => p.Valor)
            .HasPrecision(10, 2);
    }
}