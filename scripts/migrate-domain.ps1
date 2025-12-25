Write-Host "== DoaFacil | Migration Domain ==" -ForegroundColor Cyan

# Setar variável de ambiente apenas para esta sessão
$env:DOAFACIL_MYSQL_CONN="Server=127.0.0.1;Port=3307;Database=doafacil;Uid=doafacil;Pwd=doafacil123;"

Write-Host "Connection string configurada via variável de ambiente." -ForegroundColor Green

# Criar migration (se ainda não existir)
dotnet tool run dotnet-ef migrations add InitialDomain `
  --project src/DoaFacil.Infrastructure `
  --context AppDbContext `
  --output-dir Persistence/Migrations

if ($LASTEXITCODE -ne 0) {
    Write-Host "Erro ao criar migration." -ForegroundColor Red
    exit 1
}

Write-Host "Migration criada com sucesso." -ForegroundColor Green

# Aplicar no banco
dotnet tool run dotnet-ef database update `
  --project src/DoaFacil.Infrastructure `
  --context AppDbContext

if ($LASTEXITCODE -ne 0) {
    Write-Host "Erro ao aplicar migration." -ForegroundColor Red
    exit 1
}

Write-Host "Banco atualizado com sucesso." -ForegroundColor Green
