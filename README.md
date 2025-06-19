# 📘 Portal Administrativo

> Um aplicativo Blazor Server para gestão operacional e administrativa, projetado com **Clean Architecture** e construído em **.NET 9**, oferecendo uma interface de usuário sofisticada e um gerador de código eficiente.

---

## 📄 Descrição Geral

O **Portal Administrativo** é uma solução completa para gestão interna da empresa, permitindo o controle de usuários, tarefas, relatórios em PDF, filas de jobs com Hangfire, armazenamento distribuído com MinIO e suporte a múltiplos idiomas. A interface moderna e responsiva foi desenvolvida utilizando **Blazor Server**, garantindo interatividade sem recarregar a página e mantendo a produtividade do desenvolvedor com uso exclusivo de C#.

---

## 🚀 Tecnologias Utilizadas

- [.NET 9](https://dotnet.microsoft.com/)  
- [Blazor Server](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor)  
- [Entity Framework Core](https://learn.microsoft.com/ef/core)  
- [SQL Server](https://www.microsoft.com/sql-server/)  
- [PostgreSQL](https://www.postgresql.org/)  
- [SQLite](https://www.sqlite.org/)  
- [FluentValidation](https://docs.fluentvalidation.net/)  
- [MediatR](https://github.com/jbogard/MediatR)  
- [AutoMapper](https://automapper.org/)  
- [Docker](https://www.docker.com/)  
- [SignalR](https://learn.microsoft.com/aspnet/core/signalr)  
- [Serilog](https://serilog.net/)  
- [Hangfire](https://www.hangfire.io/) – *Background jobs e agendamentos*  
- [QuestPDF](https://www.questpdf.com/) – *Geração de relatórios em PDF*  
- [MinIO](https://min.io/) – *Armazenamento de objetos compatível com S3*  
- [FluentAssertions](https://fluentassertions.com/) – *Testes expressivos*  
- [Moq](https://github.com/moq/moq4) – *Mocking de dependências*  
- [Respawn](https://github.com/jbogard/Respawn) – *Reset de banco para testes*  
- **Suporte a múltiplos idiomas** via `IStringLocalizer` / `resx`

---

## 🏗️ Arquitetura do Projeto

O projeto segue os princípios da **Clean Architecture**, com separação clara de responsabilidades entre as camadas:

```
PortalAdministrativo/
├── Application/        # Casos de uso, comandos e queries
├── Domain/             # Entidades, regras de negócio e contratos
├── Infrastructure/     # Implementações de banco, serviços externos, MinIO, Hangfire
├── Server.UI/          # Projeto Blazor Server com pages, SignalR, internacionalização
└── tests/              # Testes unitários, integração e mocks
```

---

## 🧠 Padrões e Design de Projeto

- **CQRS** (Command Query Responsibility Segregation)
- **Mediator Pattern** (via MediatR)
- **Repository Pattern**
- **Unit of Work**
- **Dependency Injection (DI)**
- **Factory Pattern**
- **Princípios SOLID**
- **Internacionalização** (I18n com `IStringLocalizer`)
- **Middlewares customizados**
- **Domain Events**

---

## ⚙️ Como Executar o Projeto

1. **Clone o repositório:**

   ```bash
   git clone https://github.com/suaempresa/PortalAdministrativo.git  
   cd PortalAdministrativo
   ```

2. **Configure os arquivos:**

   - `appsettings.json`
   - `launchSettings.json`

3. **Execute as migrações do banco:**

   ```bash
   dotnet ef database update
   ```

4. **Execute o projeto localmente:**

   ```bash
   dotnet run --project Server.UI
   ```

5. **Acesse a aplicação:**

   [https://localhost:5001](https://localhost:5001)

---

## 🛠️ Docker Compose HTTPS Deployment

```bash
dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\StoreDashboard.Blazor.Server.UI.pfx -p Password@123
dotnet dev-certs https --trust

dotnet user-secrets init
dotnet user-secrets -p Server.UI.csproj set "Kestrel:Certificates:Development:Password" "Password@123"
```

---

## 🗃️ Bancos de Dados Suportados

- **PostgreSQL** → `"postgresql"`
- **Microsoft SQL Server** → `"mssql"`
- **SQLite** → `"sqlite"`

### Como trocar o banco:

1. Edite o arquivo `appsettings.json`.
2. Altere `DBProvider` para o desejado.
3. Atualize `ConnectionString`.

---

## 🔐 Variáveis de Ambiente

| Variável                     | Descrição                             |
| ---------------------------- | ------------------------------------- |
| `ConnectionStrings__Default` | String de conexão padrão              |
| `Jwt__Secret`                | Chave para geração de tokens JWT      |
| `Email__SmtpServer`          | Endereço do SMTP                      |
| `Email__Port`                | Porta usada para envio de e-mails     |
| `Serilog__MinimumLevel`      | Nível de log (ex: Debug, Information) |
| `Minio__AccessKey`           | Chave de acesso para MinIO            |
| `Minio__SecretKey`           | Chave secreta para MinIO              |

---

## 📁 Estrutura de Pastas

| Pasta             | Descrição                                                         |
| ----------------- | ----------------------------------------------------------------- |
| `/Application`    | Casos de uso, CQRS, validações                                    |
| `/Domain`         | Regras de negócio, eventos, contratos                             |
| `/Infrastructure` | Banco de dados, serviços externos, MinIO, Hangfire, QuestPDF      |
| `/Server.UI`      | Blazor Server, páginas, componentes, SignalR, internacionalização |
| `/tests`          | Testes automatizados com FluentAssertions, Moq e Respawn          |

---

🚀💻 Tecnologias e Ferramentas
<p align="left">

 <img src="https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white" />
 <img src="https://img.shields.io/badge/Visual_Studio_Code-0078D4?style=for-the-badge&logo=visual%20studio%20code&logoColor=white" />
 
 <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" />
 <img src="https://img.shields.io/badge/HTML5-E34F26?style=for-the-badge&logo=html5&logoColor=white" />
 <img src="https://img.shields.io/badge/json-5E5C5C?style=for-the-badge&logo=json&logoColor=white" />
 <img src="https://img.shields.io/badge/JavaScript-323330?style=for-the-badge&logo=javascript&logoColor=F7DF1E" />
 <img src="https://img.shields.io/badge/WebAssembly-654FF0?style=for-the-badge&logo=WebAssembly&logoColor=white" />

  <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/Blazor-512BD4?style=for-the-badge&logo=blazor&logoColor=white" />
  <img src="https://img.shields.io/badge/Entity_Framework_Core-68217A?style=for-the-badge&logo=.net&logoColor=white" />
  <img src="https://img.shields.io/badge/MediatR-000000?style=for-the-badge&logo=mediatr&logoColor=white" />
  <img src="https://img.shields.io/badge/AutoMapper-DD0031?style=for-the-badge&logo=automapper&logoColor=white" />
  <img src="https://img.shields.io/badge/Serilog-0E0E0E?style=for-the-badge&logo=serilog&logoColor=white" />
  <img src="https://img.shields.io/badge/FluentValidation-3E7FC1?style=for-the-badge&logo=fluentvalidation&logoColor=white" />
  <img src="https://img.shields.io/badge/Hangfire-6DB33F?style=for-the-badge&logo=hangfire&logoColor=white" />
  <img src="https://img.shields.io/badge/QuestPDF-FF6F61?style=for-the-badge&logo=pdf&logoColor=white" />
  <img src="https://img.shields.io/badge/MinIO-CF2E2E?style=for-the-badge&logo=minio&logoColor=white" />
  <img src="https://img.shields.io/badge/Moq-009688?style=for-the-badge&logo=moq&logoColor=white" />
  <img src="https://img.shields.io/badge/FluentAssertions-2088FF?style=for-the-badge&logo=fluentassertions&logoColor=white" />
  <img src="https://img.shields.io/badge/Respawn-5C2D91?style=for-the-badge&logo=database&logoColor=white" />
  <img src="https://img.shields.io/badge/Multi--Language_Support-FFB400?style=for-the-badge&logo=language&logoColor=white" />
 <img src="https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white" />
  <img src="https://img.shields.io/badge/Sqlite-003B57?style=for-the-badge&logo=sqlite&logoColor=white" />
   <img src="https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white" />
</p>