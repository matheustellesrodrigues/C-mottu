# 🚀 API RESTful de Operações de Comércio com Oracle (.NET)

Projeto feito para o desafio da disciplina de Advanced Business Development with .NET.

## ✅ Funcionalidades

- CRUD completo para entidades Empresa e Operacao
- Banco de dados Oracle integrado
- Relacionamento entre entidades
- Swagger UI
- 5 registros significativos inseridos
- Containerização com Docker
- Deploy com Azure CLI

## 🔗 Rotas principais

| Método | Rota             | Ação                             |
|--------|------------------|----------------------------------|
| GET    | /empresa         | Listar empresas                  |
| POST   | /empresa         | Criar nova empresa               |
| PUT    | /empresa/{id}    | Atualizar empresa por ID         |
| DELETE | /empresa/{id}    | Deletar empresa por ID           |
| GET    | /operacao        | Listar operações comerciais      |
| POST   | /operacao        | Criar operação vinculada à empresa |

## 🐳 Rodando localmente com Docker

```bash
git clone https://github.com/matheusrodrigues/projeto-api-oracle.git
cd projeto-api-oracle

docker-compose up --build
