using FluentValidation;
using TesteDBM.Models;
using TesteDBM.Repositories;

namespace TesteDBM.Validates;

public class ProdutosValidate : AbstractValidator<Produtos>
{
	private readonly IProdutosRepository _repository;

	public ProdutosValidate(IProdutosRepository repository)
	{
		_repository = repository;

		// Validação para o campo Nome: obrigatório, máximo de 100 caracteres e único no sistema
		RuleFor(p => p.Nome)
				.NotEmpty().WithMessage("O nome é obrigatório.")
				.MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.")
				.MustAsync(async (nome, cancellation) => !await _repository.IsNomeDuplicadoAsync(nome))
				.WithMessage("O nome não pode ser duplicado.");

		// Validação para o campo Preco: deve ser maior que zero e não pode ser nulo
		RuleFor(p => p.Preco)
				.GreaterThan(0).WithMessage("O preço deve ser maior que zero.")
				.NotEmpty().WithMessage("O preço é obrigatório.");
	}
}
