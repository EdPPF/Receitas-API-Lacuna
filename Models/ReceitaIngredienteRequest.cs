namespace api_receitas.Models;

public record ReceitaIngredienteRequest(Guid IngredienteId, string Quantidade);

public record ReceitaIngredienteUpdateRequest(string Quantidade); // para atualizar a quantidade do ingrediente na receita
