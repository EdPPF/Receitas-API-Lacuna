# **API de receitas**

- CRUD de ingredientes e receitas;
- Os ingredientes devem conter, pelo menos, um nome e uma unidade padrão de medida;
- Cada receita deve incluir os ingredientes registados e as respetivas quantidades;
- O modo de preparo pode ser descrito num único campo de texto.

Utilizando **.NET 8.0** e **minimal API** e *EntityFrameworkCore*. Banco de dados com SQLite.

As entidades são *Receita*, *Ingrediente* e *ReceitaIngrediente*. Esta última é a join table para a relação *many to many* entra as duas anteriores.
Como a join table possui um parâmentro adicional em sua model, foi necessário criá-la como uma classe própria, com seus métodos e rotas.

## Modificação para github actions
