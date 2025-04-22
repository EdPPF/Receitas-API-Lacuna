namespace api_receitas.Models;

public record ReceitaRequest(string Nome, string ModoPreparo, int TempoPreparo, int Porcoes);
