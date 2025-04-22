using Microsoft.EntityFrameworkCore;

namespace api_receitas.Models;

public class ReceitaModel(string nome, string modoPreparo, int tempoPreparo, int porcoes)
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string? Nome { get; private set; } = nome;
    public string? ModoPreparo { get; private set; } = modoPreparo;
    public int TempoPreparo { get; private set; } = tempoPreparo;
    public int Porcoes { get; private set; } = porcoes;
    public bool IsAtivo { get; set; } = true; // Para soft delete na base de dados
    public ICollection<ReceitaIngredienteModel>? ReceitaIngredientes { get; set; } = [];
    public List<IngredienteModel>? Ingredientes { get; set; } = []; // Para o retorno da receita com os ingredientes

    public void Atualizar(string nome, string modoPreparo, int tempoPreparo, int porcoes)
    {
        Nome = nome;
        ModoPreparo = modoPreparo;
        TempoPreparo = tempoPreparo;
        Porcoes = porcoes;
    }
    public void Desativar()
    {
        IsAtivo = false;
    }
}
