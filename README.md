# 🚀 SWAPI - API .NET de Star Wars

![.NET](https://img.shields.io/badge/.NET-8-blueviolet)
![Testes](https://img.shields.io/badge/Testes-MSTest-green)
![Autenticação](https://img.shields.io/badge/Autentica%C3%A7%C3%A4o-JWT-red)

Este é um projeto de API RESTful completo construído com .NET Minimal APIs, focado em alta qualidade de código, segurança e testabilidade. A API gerencia dados do universo Star Wars, incluindo Personagens, Planetas e Filmes, com um sistema de autenticação robusto baseado em papéis (Roles).

O projeto foi estruturado para demonstrar as melhores práticas de desenvolvimento back-end, incluindo uma arquitetura limpa (endpoints, serviços, infra), gerenciamento seguro de configurações e uma cobertura de testes de integração abrangente.

---

## ✨ Features

* **Arquitetura Limpa:** O código é organizado em `Controllers` (Endpoints), `Domain` (Serviços e Entidades) e `Infra` (Banco de Dados).
* **Autenticação e Autorização:** Sistema de login seguro usando **JWT (JSON Web Tokens)** com dois níveis de acesso (Roles):
    * **`Adm`**: Acesso total a todos os endpoints (CRUD completo).
    * **`Viewer`**: Acesso apenas a endpoints de leitura (`GET`).
* **Gerenciamento de Segredos:** Configuração de segurança profissional usando o **`.NET User Secrets`** para proteger a chave do JWT e a String de Conexão.
* **Relações Complexas de Banco:** Uso do Entity Framework Core para gerenciar relações **Um-para-Muitos** (Planeta ➔ Personagens) e **Muitos-para-Muitos** (Personagens ⟺ Filmes).
* **Database Seeding:** O banco de dados é populado automaticamente (`OnModelCreating`) com dados iniciais (admins, planetas, filmes, personagens e suas relações).
* **Suíte de Testes Abrangente:** O projeto inclui:
    * **Testes de Serviço:** Usando **SQLite em memória** (`:memory:`) para testar a lógica de negócios em isolamento.
    * **Testes de API End-to-End:** Usando `WebApplicationFactory` para simular requisições HTTP reais e testar todo o fluxo da API.

---

## 🚀 Como Executar o Projeto (Guia de Setup)

Para rodar este projeto, você **deve** configurar os segredos locais primeiro.

### 1. Pré-requisitos

* [.NET SDK](https://dotnet.microsoft.com/download) (versão 8, mas 7+ deve funcionar)
* Uma IDE (Visual Studio 2022 ou JetBrains Rider) ou um editor (VS Code)

### 2. Clone o Repositório

## 3. Configure os Segredos (Obrigatório)

Este projeto **não armazena chaves nem strings de conexão** no código-fonte por motivos de segurança.  
Você deve configurá-los usando o **.NET User Secrets**.

### 3.1. Navegue até a pasta da API

### 4. Crie o Banco e Rode a API
Depois que os segredos estão configurados, você pode rodar o projeto.

```bash
2. git clone [https://github.com/GuilhermeGomes00/SWAPI_Minimal.git](https://github.com/GuilhermeGomes00/SWAPI_Minimal.git)
cd SWAPI_Minimal```

3.1 cd Swapi/API

3.2 Inicialize o User Secrets
dotnet user-secrets init

3.3 Adicione a chave JWT
dotnet user-secrets set "Jwt:Key" "MINHA_CHAVE_SECRETA_SUPER_LONGA_E_SEGURA_123456"

4. Crie as Migrations
dotnet ef database update

Rode a API
dotnet run



