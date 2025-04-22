namespace api_receitas.Models;

public class IngredienteModel(string nome, string unidade)
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string? Nome { get; private set; } = nome;
    public string? Unidade { get; private set; } = unidade;
    public bool IsAtivo { get; set; } = true; // Para soft delete na base de dados
    public ICollection<ReceitaIngredienteModel>? ReceitaIngredientes { get; set; } = new List<ReceitaIngredienteModel>();
    public List<ReceitaModel>? Receitas { get; set; } = []; // Para o retorno do ingrediente com as receitas

    public void Atualizar(string nome, string unidade)
    {
        Nome = nome;
        Unidade = unidade;
    }
    public void Desativar()
    {
        IsAtivo = false;
    }
}
