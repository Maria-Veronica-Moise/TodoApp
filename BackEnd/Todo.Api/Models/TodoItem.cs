namespace Todo.Api.Models;

public class TodoItem
{
    public TodoItem(string title)
    {

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Cannot be empty", nameof(title));
        }

        Id = Guid.NewGuid();
        Title = title;
        Status = TodoItemStatus.New;
        CreatedAt = DateTime.Now;
    }

    public Guid Id { get; set; }

    public string Title { get; set; } = "";

    public TodoItemStatus Status { get; private set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? CompletedAt { get; private set; }

    public void MarkAsCompleted()
    {
        Status = TodoItemStatus.Completed;
        CompletedAt = DateTime.Now;
    }
}
