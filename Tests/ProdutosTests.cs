/*
 * Nome: Lucas Freitas
 * Data: 16/01/2025
 * Classe que realiza testes unitários de Produtos
 */

using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TesteDBM.Models;
using TesteDBM.Repositories;
using TesteDBM.Validates;
using Xunit;

namespace TesteDBM.Tests;

public class ProdutosTests
{
	private readonly ProdutosValidate _validator;
	private readonly Mock<IProdutosRepository> _mockRepository;

	public ProdutosTests()
	{
		_mockRepository = new Mock<IProdutosRepository>();
		_validator = new ProdutosValidate(_mockRepository.Object);
	}

	[Fact]
	public async Task Nome_DeveSerObrigatorio()
	{
		var produto = new Produtos { Nome = "", Preco = 10.5m };
		var result = await _validator.TestValidateAsync(produto);
		result.ShouldHaveValidationErrorFor(p => p.Nome).WithErrorMessage("O nome é obrigatório.");
	}

	[Fact]
	public async Task Nome_NaoDeveSerDuplicado()
	{
		_mockRepository.Setup(r => r.IsNomeDuplicadoAsync(It.IsAny<string>())).ReturnsAsync(true);
		var produto = new Produtos { Nome = "Produto1", Preco = 10.5m };
		var result = await _validator.TestValidateAsync(produto);
		result.ShouldHaveValidationErrorFor(p => p.Nome).WithErrorMessage("O nome não pode ser duplicado.");
	}

	[Fact]
	public async Task Preco_DeveSerMaiorQueZero()
	{
		var produto = new Produtos { Nome = "Produto1", Preco = 0 };
		var result = await _validator.TestValidateAsync(produto);
		result.ShouldHaveValidationErrorFor(p => p.Preco).WithErrorMessage("O preço deve ser maior que zero.");
	}

	[Fact]
	public async Task DeveAdicionarProduto()
	{
		var produto = new Produtos { Nome = "Produto1", Preco = 10.5m };
		_mockRepository.Setup(r => r.AdicionarAsync(produto)).Returns(Task.CompletedTask);

		await _mockRepository.Object.AdicionarAsync(produto);

		_mockRepository.Verify(r => r.AdicionarAsync(produto), Times.Once);
	}

	[Fact]
	public async Task DeveAtualizarProduto()
	{
		var produto = new Produtos { Id = 1, Nome = "Produto Atualizado", Preco = 20.0m };
		_mockRepository.Setup(r => r.AtualizarAsync(produto)).Returns(Task.CompletedTask);

		await _mockRepository.Object.AtualizarAsync(produto);

		_mockRepository.Verify(r => r.AtualizarAsync(produto), Times.Once);
	}

	[Fact]
	public async Task DeveRemoverProduto()
	{
		const int produtoId = 1;
		_mockRepository.Setup(r => r.DeletarAsync(produtoId)).Returns(Task.CompletedTask);

		await _mockRepository.Object.DeletarAsync(produtoId);

		_mockRepository.Verify(r => r.DeletarAsync(produtoId), Times.Once);
	}

	[Fact]
	public async Task DeveObterProdutoPorId()
	{
		const int produtoId = 1;
		var produto = new Produtos { Id = produtoId, Nome = "Produto1", Preco = 10.5m };
		_mockRepository.Setup(r => r.BuscarPorIdAsync(produtoId)).ReturnsAsync(produto);

		var resultado = await _mockRepository.Object.BuscarPorIdAsync(produtoId);

		Assert.IsNotNull(resultado);
		Assert.Equals(produtoId, resultado?.Id);
		_mockRepository.Verify(r => r.BuscarPorIdAsync(produtoId), Times.Once);
	}

	[Fact]
	public async Task DeveObterTodosProdutos()
	{
		var produtos = new List<Produtos>
						{
								new Produtos { Id = 1, Nome = "Produto1", Preco = 10.5m },
								new Produtos { Id = 2, Nome = "Produto2", Preco = 15.0m }
						};

		_mockRepository.Setup(r => r.BuscarTodosAsync()).ReturnsAsync(produtos);

		var resultado = await _mockRepository.Object.BuscarTodosAsync();

		Assert.IsNotNull(resultado);
		Assert.Equals(2, resultado.Count());
		_mockRepository.Verify(r => r.BuscarTodosAsync(), Times.Once);
	}
}




