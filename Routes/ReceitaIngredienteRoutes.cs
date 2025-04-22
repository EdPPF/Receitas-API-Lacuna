using api_receitas.Data;
using api_receitas.Models;
using Microsoft.EntityFrameworkCore;

namespace api_receitas.Routes;

public static class ReceitaIngredienteRoute
{
    public static void ReceitaIngredienteRoutes(this WebApplication app)
    {
        var routes = app.MapGroup("/receitas/{receitaId:guid}/ingredientes");

        routes.MapPost("/", async (Guid receitaId, ReceitaIngredienteRequest req, AppDbContext context) =>
        {
            var receita = await context.Receita.FindAsync(receitaId);
            var ingrediente = await context.Ingrediente.FindAsync(req.IngredienteId);
            if (receita is null || ingrediente is null)
            {
                return Results.NotFound("Receita ou ingrediente não encontrado.");
            }

            var relacao = new ReceitaIngredienteModel(
                req.Quantidade,
                receitaId,
                req.IngredienteId
            );

            await context.ReceitaIngrediente.AddAsync(relacao);
            await context.SaveChangesAsync();

            return Results.Created($"/receitas/{receitaId}/ingredientes/{ingrediente.Id}", relacao);
        });

        routes.MapGet("/", async (Guid receitaId, AppDbContext context) =>
        {
            var lista = await context.ReceitaIngrediente
                .Where(ri => ri.ReceitaId == receitaId)
                .Include(ri => ri.Ingrediente)
                .ToListAsync();

            if (lista.Count == 0)
            {
                return Results.NotFound("Nenhum ingrediente encontrado para esta receita.");
            }

            return Results.Ok(lista);
        });

        routes.MapGet("/{ingredienteId:guid}", async (
            Guid receitaId,
            Guid ingredienteId,
            AppDbContext context) =>
        {
            var relacao = await context.ReceitaIngrediente
                .Include(ri => ri.Ingrediente)
                .FirstOrDefaultAsync(ri => ri.ReceitaId == receitaId && ri.IngredienteId == ingredienteId);

            if (relacao is null)
            {
                return Results.NotFound("Relação entre receita e ingrediente não encontrada.");
            }

            return Results.Ok(relacao);
        });

        routes.MapPut("/{ingredienteId:guid}", async (
            Guid receitaId,
            Guid ingredienteId,
            ReceitaIngredienteUpdateRequest req,
            AppDbContext context) =>
        {
            var relacao = await context.ReceitaIngrediente
                .FirstOrDefaultAsync(ri => ri.ReceitaId == receitaId && ri.IngredienteId == ingredienteId);

            if (relacao is null)
            {
                return Results.NotFound("Relação entre receita e ingrediente não encontrada.");
            }

            relacao.Atualizar(req.Quantidade);
            await context.SaveChangesAsync();

            return Results.Ok(relacao);
        });

        routes.MapDelete("/{ingredienteId:guid}", async (
            Guid receitaId,
            Guid ingredienteId,
            AppDbContext context) =>
        {
            var relacao = await context.ReceitaIngrediente
                .FirstOrDefaultAsync(ri => ri.ReceitaId == receitaId && ri.IngredienteId == ingredienteId);

            if (relacao is null)
            {
                return Results.NotFound("Relação entre receita e ingrediente não encontrada.");
            }

            context.ReceitaIngrediente.Remove(relacao); // Hard delete
            await context.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}
