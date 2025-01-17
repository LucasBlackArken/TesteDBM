/*
 * Nome: Lucas Freitas
 * Data: 16/01/2025
 * Essa interface define o contrato para o repositório de Produto, 
 * garantindo a abstração e permitindo diferentes implementações.
 */

using TesteDBM.Models;

namespace TesteDBM.Repositories;

public interface IProdutosRepository
{
	Task<IEnumerable<Produtos>> BuscarTodosAsync();
	Task<Produtos?> BuscarPorIdAsync(int id);
	Task AdicionarAsync(Produtos produto);
	Task AtualizarAsync(Produtos produto);
	Task DeletarAsync(int id);
	Task<bool> IsNomeDuplicadoAsync(string nome);
}



