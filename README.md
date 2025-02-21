# Developer Evaluation

Este projeto é uma avaliação para desenvolvedores, que inclui um backend construído com .NET e um frontend em Angular. O objetivo é demonstrar habilidades em desenvolvimento full-stack, incluindo a criação de uma aplicação web interativa com um CRUD completo.

## Sequência de Desenvolvimento do Projeto

1. **Banco de Dados**: O banco de dados foi implementado usando **PostgreSQL** (ou **MongoDB**), com mapeamento objeto-relacional utilizando **Entity Framework Core** e **AutoMapper**.
2. **Classes de Manipulação de Dados**: As classes para manipulação dos dados foram implementadas usando os padrões de design **Mediator** e **AutoMapping**.
3. **Camada de Serviços**: A camada de serviços foi implementada utilizando **Rebus** para gerenciamento de mensagens.
4. **Regras de Negócio**: As regras de negócio foram desenvolvidas e integradas ao sistema.
5. **Registro em Application Log**: O registro em logs da aplicação foi implementado para monitoramento e auditoria.
6. **Testes Manuais do CRUD**: Testes manuais básicos do CRUD foram realizados via API, considerando filtros, paginação e ordenação (utilizando Postman).
7. **Testes de Unidade**: Testes de unidade foram conduzidos com **xUnit** para garantir a qualidade do código.

## Funcionalidades

- Visualização de produtos
- Pesquisa de produtos
- Adição e edição de produtos
- Gerenciamento de carrinho de compras
- Autenticação de usuários

## Tecnologias Utilizadas

### Backend

- .NET 8 (ou versão utilizada)
- Entity Framework Core
- PostgreSQL (ou MongoDB)
- AutoMapper
- Rebus
- xUnit para testes

### Frontend

- Angular
- TypeScript
- HTML/CSS
- RxJS
- Bootstrap (ou qualquer outra biblioteca de UI que você esteja usando)

## Pré-requisitos

### Backend

Antes de executar o backend, você precisa ter o seguinte instalado:

- [.NET SDK](https://dotnet.microsoft.com/download) (versão 6 ou superior)
- [PostgreSQL](https://www.postgresql.org/download/) ou [MongoDB](https://www.mongodb.com/try/download/community)

### Frontend

Antes de executar o frontend, você precisa ter o seguinte instalado:

- [Node.js](https://nodejs.org/) (versão 12 ou superior)
- [Angular CLI](https://angular.io/cli) (instalado globalmente)

```bash
npm install -g @angular/cli
