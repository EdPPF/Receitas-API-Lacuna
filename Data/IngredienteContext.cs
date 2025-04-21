using api_receitas.Models;
using Microsoft.EntityFrameworkCore;

namespace api_receitas.Data;

public class IngredienteContext : DbContext
{
    public DbSet<IngredienteModel> Ingrediente { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=receitas.sqlite;");
        }
        base.OnConfiguring(optionsBuilder);
    }
}
