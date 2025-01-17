# Web API para Gerenciamento de Produtos

Uma aplicação ASP.NET Core Web API com Entity Framework Core, PostgreSQL, Docker e FluentValidation.

## Descrição do Projeto
Esta aplicação permite realizar operações de CRUD em uma entidade de Produtos.

## Configuração Local
1. Clone o repositório:
   ```bash
   git clone https://github.com/seu_usuario/nome_projeto.git
   cd nome_projeto

2. Migrations Entity Framework Core:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   
3. Docker
```bash
docker pull seu_usuario/TesteDBM:1.0
docker run -p 5000:80 seu_usuario/TesteDBM:1.0
```
4.Testes Unitários 
```bash
dotnet tests
