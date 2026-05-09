using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using TodoApp.Models;

namespace TodoApp.Repositories;

public class TodoRepository
{
  
    public async Task<List<TodoItem>> LoadAsync()
    {

        // determine file path
        string currentLocation = Directory.GetCurrentDirectory();
        string fileName = "todos.json";
        string fileLocation = Path.Combine(currentLocation, fileName);

        // read from file using File.Read

        string fileText = await File.ReadAllTextAsync(fileLocation);

        // use JsonSerializer to convert
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

        await File.WriteAllTextAsync(fileLocation,content);
    }
}
