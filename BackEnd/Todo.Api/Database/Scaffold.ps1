$connectionString = "Server=(localdb)\MSSQLLocalDB;Database=TodoDb;Trusted_Connection=True;TrustServerCertificate=True;"

Set-Location ..

dotnet ef dbcontext scaffold `
    $connectionString `
    Microsoft.EntityFrameworkCore.SqlServer `
    --context TodoContext `
    --context-namespace Todo.Api.Database.Partials `
    --context-dir Database/Partials `
    --output-dir Models/Partials `
    --namespace "Todo.Api.Models" `
    --no-onconfiguring `
    --force

Set-Location Database