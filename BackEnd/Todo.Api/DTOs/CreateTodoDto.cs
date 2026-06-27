namespace Todo.Api.DTOs;

public class CreateTodoDto
{
    public string Title { get; set; }

    public Guid CategoryId { get; set; }
}