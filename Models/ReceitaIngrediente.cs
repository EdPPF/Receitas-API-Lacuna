namespace api_receitas.Models;

public class ReceitaIngredienteModel(string quantidade, Guid receitaId, Guid ingredienteId)
{
    public Guid ReceitaId { get; private set; } = receitaId;
    public ReceitaModel Receita { get; private set; } = null!;
    public Guid IngredienteId { get; private set; } = ingredienteId;
    public IngredienteModel Ingrediente { get; private set; } = null!;

    public string? Quantidade { get; private set; } = quantidade;

    public void Atualizar(string quantidade)
    {
        Quantidade = quantidade;
    }
    // public void Desativar()
    // {
    //     Quantidade = null;
    // }
}
