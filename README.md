# 🏘️ MsResidents - Microserviço de Controle de Moradores

Serviço independente e especializado para o controle e gerenciamento de moradores, utilizando arquitetura RESTful com ASP.NET Core, Entity Framework Core e suporte a banco de dados relacional.

---

## 📌 Visão Geral

O **MsResidents** é parte de uma arquitetura baseada em microserviços, sendo responsável exclusivamente pelo cadastro, edição, listagem e remoção de moradores. Ideal para sistemas de portaria, condomínios ou residenciais com múltiplas unidades.

---

## 🚀 Tecnologias Utilizadas

- ✅ **ASP.NET Core** 7.0+
- ✅ **Entity Framework Core**
- ✅ **MySQL / SQL Server** (relacional)
- ✅ **AutoMapper**
- ✅ **Swagger (Swashbuckle)**
- ✅ **CORS Configurado**
- ✅ **RESTful APIs**

---

## 📂 Estrutura do Projeto

MsResidents/
├── Controllers/
│   └── ResidentsController.cs
├── Models/
│   └── Resident.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Services/
│   └── ResidentService.cs
├── DTOs/
│   ├── ResidentRequest.cs
│   └── ResidentResponse.cs
├── Program.cs
├── appsettings.json
└── MsResidents.csproj
📮 Endpoints Principais
Método	Rota	Descrição
GET	/api/residents	Lista todos os moradores
GET	/api/residents/{id}	Busca um morador por ID
POST	/api/residents	Cadastra um novo morador
PUT	/api/residents/{id}	Atualiza dados do morador
DELETE	/api/residents/{id}	Remove um morador

A documentação completa da API está disponível via Swagger em:
http://localhost:{porta}/swagger

🛠️ Como Executar Localmente
Clone o repositório:

bash
Copiar
Editar
git clone https://github.com/seu-usuario/MsResidents.git
Acesse o diretório:

bash
Copiar
Editar
cd MsResidents
Configure a connection string em appsettings.json.

Execute as migrações (opcional):

bash
Copiar
Editar
dotnet ef database update
Rode o projeto:

bash
Copiar
Editar
dotnet run
📌 Observações
Este serviço não possui autenticação/autorização própria — espera que esses controles venham de um Gateway ou microserviço de autenticação/authorization.

A arquitetura é pensada para escalar, podendo facilmente ser containerizada com Docker.

🤝 Contribuições
Sinta-se à vontade para abrir issues ou enviar pull requests. Toda ajuda é bem-vinda para melhorar este projeto! 😄

📄 Licença
Este projeto está licenciado sob a MIT License. Veja o arquivo LICENSE para mais detalhes.
