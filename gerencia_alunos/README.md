# API EscolaDB

## Tecnologias

- .NET 10
- Entity Framework Core
- SQL Server
- Docker
- JWT Authentication

## Como executar

### Subir banco de dados

```bash
docker compose up -d
```

### Executar migrations

```bash
dotnet ef database update
```

### Rodar projeto

```bash
dotnet run
```