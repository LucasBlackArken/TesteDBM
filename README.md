# Web API para Gerenciamento de Produtos

Uma aplicação ASP.NET Core Web API com Entity Framework Core, PostgreSQL, Docker e FluentValidation.

## Descrição do Projeto
Esta aplicação permite realizar operações de CRUD em uma entidade de Produtos.

## Configuração Local
1. Clone o repositório:
```bash
git clone https://github.com/seu_usuario/nome_projeto.git
cd nome_projeto
```

2. Migrations Entity Framework Core:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```
   
3. Docker
```bash
docker pull seu_usuario/TesteDBM:1.0
docker run -p 5000:80 seu_usuario/TesteDBM:1.0
```
4.Testes Unitários 
```bash
dotnet tests
```

## Descrição das Camadas
 -- Camada de Apresentação : Interface MVC para interagir com uma API Web.
 -- Camada de Domínio : Contém modelos e regras de negócio.
 -- Camada de Dados : Contexto do Entity Framework e migrações.
 -- Camada de Infraestrutura : Comunicação com a API Web usando HttpClient.

## Escolha de Tecnologias
ASP.NET Core : Framework robusto para criar APIs RESTful.
Entity Framework Core : ORM para acesso ao banco de dados.
FluentValidation : Validação de regras de negócio.
Docker : Conteinerização para fácil implantação e escalabilidade.

## Desafios e Soluções
 -- Validação de Negócio :
 -- Desafio : Garantir a unicidade do nome.
 -- Solução : Usar FluentValidationcom IProdutoRepository.

Desempenho :
Desafio : Evitar múltiplas consultas ao banco.
Solução : Usar AsNoTrackingem consultas somente leitura.

Plano de Testes
Explique os cenários testados:

Validação de Produto :
Nome obrigatório e único.
Preço maior que zero.

Repositórios :
Operações de CRUD no banco de dados.

Controladores :
Respostas corretas para cada endpoint (200, 400, 404, etc.).
