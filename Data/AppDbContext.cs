using api_receitas.Models;
using Microsoft.EntityFrameworkCore;

namespace api_receitas.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ReceitaModel> Receita { get; set; }
    public DbSet<IngredienteModel> Ingrediente { get; set; }
    public DbSet<ReceitaIngredienteModel> ReceitaIngrediente { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Ajustando nomes para evitar conflitos no postgres
        modelBuilder.Entity<ReceitaModel>().ToTable("receitas");
        modelBuilder.Entity<IngredienteModel>().ToTable("ingredientes");
        modelBuilder.Entity<ReceitaIngredienteModel>().ToTable("receita_ingrediente");

        modelBuilder.Entity<ReceitaIngredienteModel>()
            .HasKey(ri => new { ri.ReceitaId, ri.IngredienteId });

        modelBuilder.Entity<ReceitaIngredienteModel>()
            .HasOne(ri => ri.Receita)
            .WithMany(r => r.ReceitaIngredientes)
            .HasForeignKey(ri => ri.ReceitaId);

        modelBuilder.Entity<ReceitaIngredienteModel>()
            .HasOne(ri => ri.Ingrediente)
            .WithMany(i => i.ReceitaIngredientes)
            .HasForeignKey(ri => ri.IngredienteId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // optionsBuilder.UseSqlite("Data Source=receitas.sqlite;");
            optionsBuilder.UseNpgsql();
        }
        base.OnConfiguring(optionsBuilder);
    }

}
