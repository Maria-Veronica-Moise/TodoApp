using Microsoft.AspNetCore.Mvc;
using Todo.Api.Services;

namespace Todo.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly TodoService _todoService;

    public TodoController(TodoService todoService)
    {
        _todoService = todoService;
        _todoService.InitializeAsync().GetAwaiter().GetResult();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var todos = _todoService.GetAll();
        return Ok(todos);
    }

    [HttpPost]
    public async Task<IActionResult> SaveTodo([FromBody] TodoDto todo)
    {
        return Ok(todo);
    }
}

public class TodoDto
{
    public string Title { get; set; }
}