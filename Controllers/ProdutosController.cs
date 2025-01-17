/*
 * Nome: Lucas Freitas
 * Data: 16/01/2025
 * Esse controlador gerencia as requisições HTTP relacionadas à entidade Produto, 
 * aplicando validações e chamando o repositório.
 */


using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using TesteDBM.Models;
using TesteDBM.Repositories;

namespace TesteDBM.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
	private readonly IProdutosRepository _repository;
	private readonly IValidator<Produtos> _validator;

	public ProdutosController(IProdutosRepository repository, IValidator<Produtos> validator)
	{
		_repository = repository;
		_validator = validator;
	}

	[HttpGet]
	public async Task<IActionResult> ListarProdutos()
	{
		var produtos = await _repository.BuscarTodosAsync();
		return Ok(produtos);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> BuscarProdutosPorId(int id)
	{
		var produto = await _repository.BuscarPorIdAsync(id);
		if (produto == null)
			return NotFound();

		return Ok(produto);
	}

	[HttpPost]
	public async Task<IActionResult> AdicionarProdutos([FromBody] Produtos produto)
	{
		ValidationResult result = await _validator.ValidateAsync(produto);
		if (!result.IsValid)
			return BadRequest(result.Errors);

		await _repository.AdicionarAsync(produto);
		return CreatedAtAction(nameof(BuscarProdutosPorId), new { id = produto.Id }, produto);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> AtualizarProdutos(int id, [FromBody] Produtos produto)
	{
		if (id != produto.Id)
			return BadRequest("ID incompativel");

		ValidationResult result = await _validator.ValidateAsync(produto);
		if (!result.IsValid)
			return BadRequest(result.Errors);

		var existingProduto = await _repository.BuscarPorIdAsync(id);
		if (existingProduto == null)
			return NotFound();

		await _repository.AtualizarAsync(produto);
		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeletarProdutos(int id)
	{
		var produto = await _repository.BuscarPorIdAsync(id);
		if (produto == null)
			return NotFound();

		await _repository.DeletarAsync(id);
		return NoContent();
	}
}
