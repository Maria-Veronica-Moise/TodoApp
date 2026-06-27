using System.Text.Json.Serialization;

namespace Todo.Api.Models;

public class TodoItem
{
    public TodoItem(string title, Guid categoryId)
    {

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Cannot be empty", nameof(title));
        }

        Id = Guid.NewGuid();
        Title = title;
        CategoryId = categoryId;
        Status = TodoItemStatus.New;
        CreatedAt = DateTime.Now;
    }

    public Guid Id { get; set; }

    public string Title { get; set; } = "";

    [JsonInclude]
    public Guid CategoryId { get; private set; }

    [JsonInclude]
    public TodoItemStatus Status { get; private set; }

    public DateTime CreatedAt { get; set; }

    [JsonInclude]
    public DateTime? CompletedAt { get; private set; }

    public void MarkAsCompleted()
    {
        Status = TodoItemStatus.Completed;
        CompletedAt = DateTime.Now;
    }

    public void MarkAsPending()
    {
        Status = TodoItemStatus.New;
        CompletedAt = null;
    }
}
