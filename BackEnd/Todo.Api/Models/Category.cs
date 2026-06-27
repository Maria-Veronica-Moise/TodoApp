using System.Text.Json.Serialization;

namespace Todo.Api.Models;

public class Category
{
    [JsonInclude]
    public Guid Id { get; private set; }

    [JsonInclude]
    public string Name { get; private set; }

    public Category(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}
