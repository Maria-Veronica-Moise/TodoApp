namespace Todo.Api.DTOs;

public class TodoSummaryDto 
{
    public Guid Id { get; set; }

    public int Order { get; set; }

    public string Title { get; set; } = "";

    public bool IsCompleted { get; set; }
}