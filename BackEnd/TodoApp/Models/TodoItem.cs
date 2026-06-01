namespace TodoApp.Models;

public class TodoItem
{
    public TodoItem(string title, string description, string assignedTo)
    {

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Cannot be empty", nameof(title));
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Cannot be empty", nameof(description));
        }

        if (string.IsNullOrWhiteSpace(assignedTo))
        {
            throw new ArgumentException("Cannot be empty", nameof(assignedTo));
        }
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Status = TodoItemStatus.New;
        CreatedAt = DateTime.Now;
        AssignedTo = assignedTo;
    }
    public Guid Id { get; set; }

    public string Title { get; set; } = "";

    public string Description { get; set; } = "";

    public TodoItemStatus Status { get; private set; }
    public DateTime CreatedAt { get; set; }

    public DateTime? CompletedAt { get; private set; }

    public string AssignedTo { get; set; } = "";

    public void MarkAsCompleted()
    {
        Status = TodoItemStatus.Completed;
        CompletedAt = DateTime.Now;
    }
}
