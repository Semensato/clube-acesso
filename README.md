# 🏟️ ClubeAcesso.API

API REST para controle de acesso a áreas de um clube. Desenvolvido em .NET 8 com Entity Framework (Code First), arquitetura em camadas e foco em boas práticas de desenvolvimento.

---

## 🚀 Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/)
- Entity Framework Core
- SQL Server (via Docker)
- SQLite (para testes ou ambiente local)
- xUnit (testes automatizados)
- Moq (mocking nos testes)
- Swagger (interface de documentação e testes HTTP)

---

## 🧱 Arquitetura

O projeto segue a arquitetura em camadas:

- **API** – Camada de apresentação (controllers)
- **Application** – Casos de uso e regras de orquestração
- **Domain** – Entidades e regras de negócio
- **Infrastructure** – Repositórios, banco de dados, EF Core
- **Tests** – Testes unitários e de integração

---

## ⚙️ Como rodar o projeto com Docker

Certifique-se de que você tem o [Docker](https://www.docker.com/) e o [Docker Compose](https://docs.docker.com/compose/) instalados.

### ▶️ Subir o ambiente:

```bash
docker-compose up --build
```

> Isso irá:
> - Construir a aplicação
> - Subir o container do SQL Server
> - Aplicar as migrations (se configurado)
> - Disponibilizar a API em `http://localhost:5000`

---

## 🔍 Acessar a documentação Swagger

Após subir a aplicação, acesse:

```
http://localhost:5000/swagger
```

---

## 🧪 Executar Testes Automatizados

O projeto já contém testes unitários que validam a principal regra de negócio:

> "Um sócio só pode acessar áreas permitidas por seu plano."

### ▶️ Rodar os testes:

```bash
dotnet test
```

> Os testes estão localizados na pasta `tests/ClubeAcesso.Tests`.

---

## 📁 Estrutura de Diretórios

```
src/
  ClubeAcesso.API/               → Camada de apresentação
  ClubeAcesso.Application/       → Casos de uso e serviços
  ClubeAcesso.Domain/            → Entidades, enums, validações
  ClubeAcesso.Infrastructure/    → EF Core, repositórios, DbContext
tests/
  ClubeAcesso.Tests/             → Testes unitários (xUnit, Moq)
docker-compose.yml               → Infra com SQL Server + API
```

---

## 🧠 Decisões de Projeto

- Separação por camadas para melhor organização e manutenibilidade.
- Entity Framework Code-First para controle de schema versionado.
- Regra de acesso isolada na camada de domínio/serviço de aplicação.
- Testes unitários cobrindo as regras mais importantes (ex: autorização de acesso).
- Documentação interativa via Swagger para facilitar testes manuais.

---

## 📌 Exemplo de Regra Implementada

- Sócio só pode acessar áreas permitidas no plano.
- Toda tentativa é registrada com:
  - ID do sócio
  - ID da área
  - Data/hora da tentativa
  - Resultado (Autorizado ou Negado)

---

## 📬 Contato

Desenvolvido por **Guilherme Semensato**  
📧 seu-email@email.com  
📎 GitHub: [https://github.com/seu-usuario](https://github.com/seu-usuario)
