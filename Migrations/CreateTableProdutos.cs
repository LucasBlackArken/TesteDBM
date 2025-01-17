using FluentMigrator;

namespace TesteDBM.Migrations;


[Migration(20250116)]
public class CreateTableProdutos : Migration
{
	public override void Up()
	{
		// Cria a tabela Produtos no banco de dados com colunas Id, Nome, Descricao, Preco e DataCadastro
		Create.Table("Produtos")
				.WithColumn("Id").AsInt32().PrimaryKey().Identity()
				.WithColumn("Nome").AsString(100).NotNullable()
				.WithColumn("Descricao").AsString().Nullable()
				.WithColumn("Preco").AsDecimal(18, 2).NotNullable()
				.WithColumn("DataCadastro").AsDateTime().WithDefault(SystemMethods.CurrentUTCDateTime);
	}

	public override void Down()
	{
		// Remove a tabela Produtos do banco de dados
		Delete.Table("Produtos");
	}
}

