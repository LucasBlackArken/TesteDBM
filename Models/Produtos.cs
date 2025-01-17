/*
 * Nome: Lucas Freitas
 * Data: 16/01/2025
 * Esta classe representa o modelo de domínio para a entidade Produto, 
 * encapsulando suas propriedades e lógica básica.
 */

namespace TesteDBM.Models;

public class Produtos
{
	public int Id { get; set; }
	public string Nome { get; set; } = string.Empty;
	public string? Descricao { get; set; }
	public decimal Preco { get; set; }
	public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
}
