# Blazor + MVVM + Feature Slices + MediatR

Exemplo de projeto com Blazor, MVVM pattern, Feature Slices e MediatR

## Estrutura do Projeto

```text
blazor-mvvm-api/
├── App.razor
├── Program.cs
├── Routes.razor
├── _Imports.razor
├── README.md
├── appsettings.json
├── appsettings.Development.json
├── blazor-mvvm-api.csproj
├── docker-compose.yml
├── docker-compose.override.yml
├── Dockerfile
├── Dockerfile.dev
├── dotnet-tools.json
├── Makefile
├── nginx.conf
├── Behaviors/
│   └── ValidationBehavior.cs
├── Features/
│   └── Produtos/
│       ├── _Imports.razor
│       ├── Commands/
│       │   ├── DeletarProdutoValidator.cs
│       │   ├── CriarProduto/
│       │   ├── DeletarProduto/
│       │   └── EditarProduto/
│       ├── Models/
│       │   └── Produto.cs
│       ├── Queries/
│       │   ├── ListarProduto/
│       │   └── ObterProdutoPorId/
│       ├── Validators/
│       │   └── CriarProdutoValidator.cs
│       ├── ViewModels/
│       │   └── ProdutoViewModel.cs
│       └── Views/
│           ├── CriarProduto.razor
│           ├── EditarProduto.razor
│           ├── Error.razor
│           └── Produtos.razor
├── Infrastructure/
│   ├── Data/
│   │   ├── AppDbContextFactory.cs
│   │   └── ApplicationDbContext.cs
│   └── Repositories/
│       ├── IProdutoRepository.cs
│       └── ProdutoRepository.cs
├── Migrations/
│   ├── 20260824163433_InitialCreate.cs
│   ├── 20260824163433_InitialCreate.Designer.cs
│   └── ApplicationDbContextModelSnapshot.cs
├── Properties/
│   └── launchSettings.json
├── scripts/
│   └── entrypoint.sh
├── Shared/
│   ├── Components/
│   │   └── ConfirmarDelecao.razor
│   └── Layout/
│       ├── MainLayout.razor
│       ├── MainLayout.razor.css
│       ├── NavMenu.razor
│       └── NavMenu.razor.css
└── wwwroot/
    ├── app.css
    └── bootstrap/
        └── bootstrap.min.css
```

---

## Comandos para reestruturar o projeto a partir do template default

<img width="401" height="737" alt="image" src="https://github.com/user-attachments/assets/ffd45cb2-f188-4344-b272-bf5d7b16c497" />

---

## Pacotes Nuget Necessários

- FluentValidation 12.1.1
- FluentValidation.DependencyInjectionExtensions 12.1.1
- MediatR 14.2.0
- MediatR.Extensions.Microsoft.DependencyInjection 11.1.0
- Microsoft.EntityFrameworkCore.Sqlite 8.0.30
- Microsoft.EntityFrameworkCore.Tools 10.0.11

---

<img width="250" height="245" alt="image" src="https://github.com/user-attachments/assets/6555fd7c-a1a6-4c8b-bdac-082d30eea713" />

---

<img width="487" height="352" alt="image" src="https://github.com/user-attachments/assets/174cfe0f-81ef-4d5e-82e7-4de6115a9d06" />

---

## Comandos

```bash
git rm -r --cached <path>

dotnet tool install --global dotnet-ef

dotnet new tool-manifest

dotnet tool install --local dotnet-ef

# Add a migration
dotnet ef migrations add InitialCreate

# Update the database
dotnet ef database update

# Recriar o banco com dados seed
rm blazor.db
```

## Comandos Docker

```bash
# Construir imagem
docker build -t blazor-mvvm-api:latest .

# Construir com Docker Compose
docker-compose build

# Rodar em background
docker-compose up -d

# Rodar com logs
docker-compose up

# Parar
docker-compose down

Gerenciar Containers
# Ver containers rodando
docker ps

# Ver todos containers
docker ps -a

# Ver logs
docker logs blazor-mvvm-app

# Entrar no container
docker exec -it blazor-mvvm-app /bin/bash

# Ver estatísticas
docker stats blazor-mvvm-app

# Parar container
docker stop blazor-mvvm-app

# Remover container
docker rm blazor-mvvm-app

Gerenciar Imagens
# Listar imagens
docker images

# Remover imagem
docker rmi blazor-mvvm-api:latest

# Limpar imagens não utilizadas
docker image prune -f

Gerenciar Volumes
# Listar volumes
docker volume ls

# Remover volume
docker volume rm blazor-mvvm-api_Data

# Backup do volume
docker run --rm -v blazor-mvvm-api_Data:/data -v $(pwd):/backup alpine tar czf /backup/data-backup.tar.gz -C /data .

```

## Exemplo de Uso

```bash
Construir e Rodar com Docker Compose
# Clonar o projeto
git clone seu-repositorio
cd blazor-mvvm-api

# Build e start
docker-compose up -d --build

# Verificar logs
docker-compose logs -f

# Acessar a aplicação
# http://localhost:80

Rodar apenas com Docker
# Build
docker build -t blazor-mvvm-api .

# Run
docker run -d \
  --name blazor-mvvm-app \
  -p 80:80 \
  -p 443:443 \
  -v $(pwd)/Data:/app/Data \
  -e ASPNETCORE_ENVIRONMENT=Production \
  -e ConnectionStrings__DefaultConnection="Data Source=/app/Data/blazor.db" \
  blazor-mvvm-api

# Ver logs
docker logs -f blazor-mvvm-app

Desenvolvimento com Hot Reload
# Usar Dockerfile.dev
docker build -f Dockerfile.dev -t blazor-mvvm-api:dev .

docker run -d \
  --name blazor-mvvm-dev \
  -p 5000:5000 \
  -v $(pwd):/app \
  -e ASPNETCORE_ENVIRONMENT=Development \
  blazor-mvvm-api:dev
```

## Testes

```bash
dotnet test

# Testes da funcionalidade CriarProduto
dotnet test --filter "FullyQualifiedName~CriarProduto"

# Testes unitários
dotnet test --filter "Category=Unit"

# Testes de integração
dotnet test --filter "Category=Integration"

Rodar com cobertura de código
dotnet test --collect:"XPlat Code Coverage"

Rodar em modo watch (desenvolvimento)
dotnet watch test

Rodar com detalhamento
dotnet test --verbosity detailed
```
