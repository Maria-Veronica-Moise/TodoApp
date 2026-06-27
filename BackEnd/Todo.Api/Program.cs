using Microsoft.EntityFrameworkCore;
using Todo.Api.Database;
using Todo.Api.Models;
using Todo.Api.Repositories;
using Todo.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment.EnvironmentName;
builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{env}.json", optional: true);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalFrontend", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddTransient<TodoRepository>();
builder.Services.AddTransient<TodoService>();

builder.Services.AddTransient<CategoryRepository>();
builder.Services.AddTransient<CategoryService>();

builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("TodoDb")));

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName.Equals("loc", StringComparison.OrdinalIgnoreCase))
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Todo API V1");
        options.RoutePrefix = "swagger";
    });
}
app.UseCors("LocalFrontend");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
