using api_receitas.Data;
using api_receitas.Models;
using Microsoft.EntityFrameworkCore;

namespace api_receitas.Routes;

public static class IngredienteRoute
{
    public static void IngredienteRoutes(this WebApplication app)
    {
        var routes = app.MapGroup("/ingredientes");

        routes.MapPost("/", async (IngredienteRequest req, AppDbContext context) =>
        {
            var ingrediente = new IngredienteModel(req.Nome, req.Unidade);
            await context.AddAsync(ingrediente);
            await context.SaveChangesAsync();
            return Results.Created($"/ingredientes/{ingrediente.Id}", ingrediente);
        });

        routes.MapGet("/", async (AppDbContext context) =>
        {
            var ingredientes = await context.Ingrediente.ToListAsync();
            if (ingredientes.Count == 0)
            {
                return Results.NotFound("Nenhum ingrediente encontrado.");
            }
            return Results.Ok(ingredientes);
        });

        routes.MapGet("/{id:guid}", async (Guid id, AppDbContext context) =>
        {
            var ingrediente = await context.Ingrediente.FindAsync(id);
            if (ingrediente is null)
            {
                return Results.NotFound("Ingrediente não encontrado.");
            }
            return Results.Ok(ingrediente);
        });

        routes.MapPut("/{id:guid}", async (Guid id, IngredienteRequest req, AppDbContext context) =>
        {
            var ingrediente = await context.Ingrediente.FirstOrDefaultAsync(i => i.Id == id);
            if (ingrediente is null)
            {
                return Results.NotFound("Ingrediente não encontrado.");
            }
            ingrediente.Atualizar(req.Nome, req.Unidade);
            await context.SaveChangesAsync();
            return Results.Ok(ingrediente);
        });

        routes.MapDelete("/{id:guid}", async (Guid id, AppDbContext context) =>
        {
            var ingrediente = await context.Ingrediente.FirstOrDefaultAsync(i => i.Id == id);
            if (ingrediente is null)
            {
                return Results.NotFound("Ingrediente não encontrado.");
            }
            ingrediente.Desativar();
            await context.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}
