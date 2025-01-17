/*
 * Nome: Lucas Freitas
 * Data: 16/01/2025
 * Esta classe implementa o repositório de Produto, encapsulando a lógica de acesso ao banco de dados.
 */

using Microsoft.EntityFrameworkCore;
using TesteDBM.Models;
using TesteDBM.Context;
using TesteDBM.Repositories;

namespace TesteDBM.Services;

public class ProdutosRepository : IProdutosRepository
{
	private readonly AppDbContext _context;

	public ProdutosRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Produtos>> BuscarTodosAsync()
	{
		return await _context.Produtos.ToListAsync();
	}

	public async Task<Produtos?> BuscarPorIdAsync(int id)
	{
		return await _context.Produtos.FindAsync(id);
	}

	public async Task AdicionarAsync(Produtos produto)
	{
		await _context.Produtos.AddAsync(produto);
		await _context.SaveChangesAsync();
	}

	public async Task AtualizarAsync(Produtos produto)
	{
		_context.Produtos.Update(produto);
		await _context.SaveChangesAsync();
	}

	public async Task DeletarAsync(int id)
	{
		var produto = await _context.Produtos.FindAsync(id);
		if (produto != null)
		{
			_context.Produtos.Remove(produto);
			await _context.SaveChangesAsync();
		}
	}

	public async Task<bool> IsNomeDuplicadoAsync(string nome)
	{
		return await _context.Produtos.AnyAsync(p => p.Nome == nome);
	}
}
