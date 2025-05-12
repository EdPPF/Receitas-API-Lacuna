using api_receitas.Data;
using api_receitas.Models;
using Microsoft.EntityFrameworkCore;

namespace api_receitas.Routes;

public static class HealthRoutes
{
    public static void HealthRoute(this WebApplication app)
    {
        var routes = app.MapGroup("/");

        routes.MapGet("/", () =>
        {
            return Results.Ok("API de Receitas OK");
        });
    }
}
