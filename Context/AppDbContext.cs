/*
 * Nome: Lucas Freitas
 * Data: 16/01/2025
 * Essa classe é o contexto do Entity Framework Core, 
 * gerenciando a conexão com o banco de dados e mapeando as entidades.
 */

using Microsoft.EntityFrameworkCore;
using TesteDBM.Models;

namespace TesteDBM.Context;

public class AppDbContext : DbContext
{
	public DbSet<Produtos> Produtos { get; set; }

	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Produtos>(entity =>
		{
			entity.HasKey(p => p.Id);
			entity.Property(p => p.Nome)
						.IsRequired()
						.HasMaxLength(100);
			entity.HasIndex(p => p.Nome)
						.IsUnique();
			entity.Property(p => p.Preco)
						.IsRequired()
						.HasColumnType("decimal(18,2)");
			entity.Property(p => p.DataCadastro)
						.HasDefaultValueSql("GETUTCDATE()");
		});
	}
}
