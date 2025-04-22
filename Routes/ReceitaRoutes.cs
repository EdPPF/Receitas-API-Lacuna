using api_receitas.Data;
using api_receitas.Models;
using Microsoft.EntityFrameworkCore;

namespace api_receitas.Routes;

public static class ReceitaRoute
{
    public static void ReceitaRoutes(this WebApplication app)
    {
        var routes = app.MapGroup("/receitas");

        routes.MapPost("/", async (ReceitaRequest req, AppDbContext context) =>
        {
            var receita = new ReceitaModel(req.Nome, req.ModoPreparo, req.TempoPreparo, req.Porcoes);
            await context.AddAsync(receita);
            await context.SaveChangesAsync();
            return Results.Created($"/receitas/{receita.Id}", receita);
        });

        routes.MapGet("/", async (AppDbContext context) =>
        {
            var receitas = await context.Receita.ToListAsync();
            if (receitas.Count == 0)
            {
                return Results.NotFound("Nenhum receita encontrado.");
            }
            return Results.Ok(receitas);
        });

        routes.MapGet("/{id:guid}", async (Guid id, AppDbContext context) =>
        {
            var receita = await context.Receita.FindAsync(id);
            if (receita is null)
            {
                return Results.NotFound("receita não encontrado.");
            }
            return Results.Ok(receita);
        });

        routes.MapPut("/{id:guid}", async (Guid id, ReceitaRequest req, AppDbContext context) =>
        {
            var receita = await context.Receita.FirstOrDefaultAsync(i => i.Id == id);
            if (receita is null)
            {
                return Results.NotFound("receita não encontrado.");
            }
            receita.Atualizar(req.Nome, req.ModoPreparo, req.TempoPreparo, req.Porcoes);
            await context.SaveChangesAsync();
            return Results.Ok(receita);
        });

        routes.MapDelete("/{id:guid}", async (Guid id, AppDbContext context) =>
        {
            var receita = await context.Receita.FirstOrDefaultAsync(i => i.Id == id);
            if (receita is null)
            {
                return Results.NotFound("receita não encontrado.");
            }
            receita.Desativar();
            await context.SaveChangesAsync();
            return Results.NoContent();
        });
    }
}
