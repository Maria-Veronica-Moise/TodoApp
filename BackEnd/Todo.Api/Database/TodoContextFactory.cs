using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Todo.Api.Database;

//public class TodoContextFactory : IDesignTimeDbContextFactory<TodoContext>
//{
//    public TodoContext CreateDbContext(string[] args)
//    {
//        var configuration = new ConfigurationBuilder()
//            .SetBasePath(Directory.GetCurrentDirectory())
//            .AddJsonFile("appsettings.json")
//            .AddJsonFile("appsettings.loc.json", optional: true)
//            .Build();

//        var optionsBuilder = new DbContextOptionsBuilder<TodoContext>();

//        optionsBuilder.UseSqlServer(configuration.GetConnectionString("TodoDb"));

//        return new TodoContext(optionsBuilder.Options);
//    }
//}
