# DoaFacil

O processo de doação de itens físicos, embora socialmente relevante, ainda ocorre de forma majoritariamente informal, descentralizada e sem mecanismos adequados de acompanhamento. Essa realidade gera insegurança, baixa previsibilidade e reduz a efetividade das doações. O projeto DoaFácil surge com o objetivo de estruturar esse processo por meio de uma plataforma digital que permita rastrear, acompanhar e confirmar todas as etapas de uma doação, aumentando a confiabilidade e o impacto social.

---

## Arquitetura

O projeto utiliza **Clean Architecture** com **DDD Pragmático** e **ASP.NET Core MVC**.

### Estrutura de Camadas

```
Web (MVC) → Application → Domain ← Infrastructure
```

**DoaFacil.Domain** - Núcleo do negócio
- Entidades, Value Objects, Interfaces de repositórios
- Sem dependências externas

**DoaFacil.Application** - Casos de uso
- Use Cases, DTOs, Services, Validadores
- Depende de: Domain

**DoaFacil.Infrastructure** - Implementações técnicas
- Repositórios, Banco de dados, Serviços externos
- Depende de: Domain, Application

**DoaFacil.Web** - Interface Web
- Controllers, Views, ViewModels
- Depende de: Application, Infrastructure

### Stack Tecnológico

- .NET 8.0
- ASP.NET Core MVC
- Bootstrap, jQuery
- Dependency Injection nativo

---

## ✅ Pré-requisitos

Antes de começar, certifique-se de ter instalado:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) ou superior
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/)

---

## Como Configurar e Executar

### 1. Clone e Restaure

```bash
git clone https://github.com/IgorSalvador/DoaFacil.git
cd DoaFacil
dotnet restore
```

### 2. Configure o Banco de Dados

**[INSTRUÇÕES DE CONFIGURAÇÃO DO BANCO DE DADOS]**

```bash
# Exemplo:
# 1. Atualize a connection string em appsettings.json
# 2. Execute as migrations:
dotnet ef database update --project DoaFacil.Infrastructure --startup-project DoaFacil.Web
```

### 3. Execute o Projeto

```bash
cd DoaFacil.Web
dotnet run
```

Acesse: `https://localhost:7xxx` ou `http://localhost:5xxx`

## Padrões Utilizados

- **Clean Architecture** - Separação em camadas independentes
- **SOLID** - Princípios de design orientado a objetos
- **DDD** - Modelagem rica de domínio
- **Dependency Injection** - Inversão de controle

---