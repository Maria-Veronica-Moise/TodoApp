using System.Text.Json;
using Todo.Api.Models;

namespace Todo.Api.Repositories;

public class CategoryRepository
{
    public async Task<List<Category>> LoadAsync()
    {
        string currentLocation = Directory.GetCurrentDirectory();
        string fileName = "categories.json";
        string fileLocation = Path.Combine(currentLocation, fileName);

        if (!File.Exists(fileLocation))
        {
            var categories = JsonSerializer.Serialize(new List<Category>());
            await File.WriteAllTextAsync(fileLocation, categories);
        }

        string fileText = await File.ReadAllTextAsync(fileLocation);

        JsonSerializerOptions jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        List<Category>? categoriesList =
            JsonSerializer.Deserialize<List<Category>>(fileText, jsonSerializerOptions);

        return categoriesList;
    }

    public async Task SaveAsync(IEnumerable<Category> categories)
    {
        string currentLocation = Directory.GetCurrentDirectory();
        string fileName = "categories.json";
        string fileLocation = Path.Combine(currentLocation, fileName);

        string content = JsonSerializer.Serialize(categories);

        await File.WriteAllTextAsync(fileLocation, content);
    }
}
