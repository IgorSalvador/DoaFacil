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

### 2. Configure o Banco de Dados (MySQL via Docker)

O projeto utiliza MySQL como banco de dados, configurado via Docker Compose.

#### 2.1. Pré-requisito: Docker

Certifique-se de ter o [Docker Desktop](https://www.docker.com/products/docker-desktop/) instalado e em execução.

#### 2.2. Inicie o Container MySQL

Na raiz do projeto, execute:

```bash
docker-compose up -d
```

Este comando irá:
- Baixar a imagem do MySQL 8.0 (se necessário)
- Criar um container chamado `doafacil_mysql`
- Configurar o banco de dados com as seguintes credenciais:
  - **Banco de dados:** doafacil
  - **Usuário:** doafacil
  - **Senha:** doafacil123
  - **Porta:** 3307 (mapeada para a porta 3307 do host)

#### 2.3. Configure a Connection String

Atualize o arquivo `appsettings.json` ou `appsettings.Development.json` em `DoaFacil.Web`:

```json
{
  "ConnectionStrings": {
    "DoaFacilDomain": "Server=localhost;Port=3307;Database=doafacil;Uid=doafacil;Pwd=doafacil123;",
    "DoaFacilAuth":   "Server=localhost;Port=3307;Database=doafacil;Uid=doafacil;Pwd=doafacil123;"
  }
}
```

#### 2.4. Execute as Migrations

```bash
dotnet ef database update --project DoaFacil.Infrastructure --startup-project DoaFacil.Web
```

#### 2.5. Comandos Úteis do Docker

```bash
# Ver status do container
docker ps

# Parar o container
docker-compose down

# Ver logs do MySQL
docker logs doafacil_mysql

# Acessar o console do MySQL
docker exec -it doafacil_mysql mysql -u doafacil -p
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