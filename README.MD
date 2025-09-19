## 📄 Corretamente

Corretamente é um sistema de gerenciamento de documentos voltado para o setor de corretagem imobiliária.

## 🗄️ Migrations (Entity Framework Core)

Para criar as tabelas iniciais e aplicar as migrations no banco de dados, utilizei os seguintes comandos:

```shell
# Criar a migration inicial
dotnet ef migrations add CreateInitialTables \
  --project Corretamente.Infrastructure \
  --startup-project Corretamente.API \
  --output-dir Contexts/Migrations

# Aplicar as migrations no banco
dotnet ef database update \
  --project Corretamente.Infrastructure \
  --startup-project Corretamente.API
```