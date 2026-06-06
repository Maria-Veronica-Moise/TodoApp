using System.Text.Json;
using Todo.Api.Models;

namespace Todo.Api.Repositories;

public class TodoRepository
{
    public async Task<List<TodoItem>> LoadAsync()
    {
        string currentLocation = Directory.GetCurrentDirectory();
        string fileName = "todos.json";
        string fileLocation = Path.Combine(currentLocation, fileName);

        if (!File.Exists(fileLocation))
        {
            var todos = JsonSerializer.Serialize(new List<TodoItem>());
            await File.WriteAllTextAsync(fileLocation, todos);
        }

        string fileText = await File.ReadAllTextAsync(fileLocation);

        JsonSerializerOptions jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };
        List<TodoItem>? todoItems = JsonSerializer.Deserialize<List<TodoItem>>(fileText, jsonSerializerOptions);
        return todoItems;
    }

    public async Task SaveAsync(IEnumerable<TodoItem> todos)
    {
        string currentLocation = Directory.GetCurrentDirectory();
        string fileName = "todos.json";
        string fileLocation = Path.Combine(currentLocation, fileName);
        string content = JsonSerializer.Serialize(todos);

        await File.WriteAllTextAsync(fileLocation, content);
    }
}
