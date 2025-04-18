namespace api_receitas.Models;

public class IngredienteModel(string nome, string unidade)
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Nome { get; private set; } = nome;
    public string Unidade { get; private set; } = unidade;
    public bool IsAtivo { get; set; } = true; // Para soft delete na base de dados
    // public string? Descricao { get; set; }
    // public string? Tipo { get; set; }
    // public double? Quantidade { get; set; }
}
