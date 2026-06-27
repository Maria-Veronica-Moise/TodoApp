using Microsoft.EntityFrameworkCore;

namespace Todo.Api.Database;

public partial class TodoContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TodoDb;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}
