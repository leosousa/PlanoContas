# Plano de Contas API
API em .Net 6 para gerenciamento de categorias de contas

## Tecnologias utilizadas
- .Net Core 6.0
- Entity Framework Core 6
- MediatR
- AutoMapper
- FluentValidation
- CQRS
- Sql Server
- Clean Architecture
- Testes unitários

## Arquitetura
### Domínio
- Camada de domínio do sistema, contendo as entidades de negócio que fazem parte do coração do sistema
### Aplicação
- Camada de aplicação do sistema, contendo os casos de uso do sistema e a lógica da aplicação
### Infraestrutura
- Camada de infra que provê recursos utilizados pelo sistema. Ex.: Identity (para autenticação e autorização), Entity Framework (para persistência de dados)
- A API também é incluída nessa camada, como um provedor de recursos para aplicações front-end e mobile

## Getting started
- Precisa ter instalado o .Net Core 6.0 SDK
- Precisa ter instalado o Sql Server Express

## Gerando o banco de dados
- Crie o banco vazio no Sql Server.
- Após a criação do banco, atualize o arquivo appSettings.json, no caminho `ConnectionStrings\DefaultConnection`. Altere o servidor e o nome do banco
- O banco será populado sozinho ao rodar a primeira vez a aplicação

## Rodando a aplicação
- Para rodar local, rode o comando `dotnet run` no seu terminal ou clique em "Run" no Visual Studio.
- A página do Swagger deverá ser exibida
- O banco é populado automaticamente ao iniciar através da classe `ApplicationDbContextSeed.cs`, na camada de Infra.
- Um usuário administrador será gerado com as seguintes credenciais: email `administrator@localhost` e senha `Administrator1!`.
- No endpoint de autenticação (`api/Auth/login`), você pode gerar um token de autenticação passando as credenciais no item anterior.
- Gerado o token, você pode pelo Swagger clicar no botão `Authorize` e incluir o token ali
- A partir daí é possível criar, editar, listar ou remover uma conta.

## Rodando as migrations via comando
- Rode o script abaixo para rodar as migrations `update-database`. Obs.: Esse comando não executa os seeds.

## Criando novas migrations
- Abra o Console do Gerenciador de Pacotes dentro do Visual Studio ou no seu terminal
- Rode o comando `add-migration NOME_MIGRATION -OutputDir "Persistence/Migrations" -Context ApplicationDbContext`, alterando apenas a variável NOME_MIGRATION
- Uma classe com a migration deve ser gerada
- Rode o comando `update-database` para rodar as atualizações no banco