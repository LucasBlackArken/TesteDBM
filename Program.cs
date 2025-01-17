using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using TesteDBM.Context;
using TesteDBM.Repositories;
using TesteDBM.Services;
using TesteDBM.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
		options.UseNpgsql(builder.Configuration.GetConnectionString(Utils.ConnectionString)));

builder.Services.AddFluentMigratorCore()
		.ConfigureRunner(rb => rb.AddPostgres()
				.WithGlobalConnectionString(builder.Configuration.GetConnectionString(Utils.ConnectionString))
				.ScanIn(typeof(Program).Assembly).For.Migrations());



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProdutosRepository, ProdutosRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
