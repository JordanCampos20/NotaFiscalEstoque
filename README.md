dotnet ef migrations add "First_Migration" -p NotaFiscalEstoque.Infrastructure -s NotaFiscalEstoque.API

dotnet ef database update -p NotaFiscalEstoque.Infrastructure -s NotaFiscalEstoque.API