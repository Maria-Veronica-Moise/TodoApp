using System.Text.Json;
using TodoApp.Models;

namespace TodoApp.Repositories;

public class TodoRepository
{

    public async Task<List<TodoItem>> LoadAsync()
    {
        string currentLocation = Directory.GetCurrentDirectory();
        string fileName = "todos.json";
        string fileLocation = Path.Combine(currentLocation, fileName);

        string fileText = await File.ReadAllTextAsync(fileLocation);

        JsonSerializerOptions jsonSerializerOption = new()
        {
            PropertyNameCaseInsensitive = true
        };
        List<TodoItem>? todoItems = JsonSerializer.Deserialize<List<TodoItem>>(fileText, jsonSerializerOption);
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
