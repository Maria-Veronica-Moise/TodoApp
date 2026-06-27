using Microsoft.AspNetCore.Mvc;
using Todo.Api.DTOs;
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
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var todos = await _todoService.GetAll();
        return Ok(todos);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActive()
    {
        var todos = await _todoService.GetPendingTodos();
        return Ok(todos);
    }

    [HttpGet("completed")]
    public async Task<IActionResult> GetCompleted()
    {
        var todos = await _todoService.GetCompletedTodos();
        return Ok(todos);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> CompleteTodo(Guid id)
    {
        await _todoService.StatusTodoAsync(id);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> SaveTodo([FromBody] CreateTodoDto dto)
    {
        await _todoService.CreateTodoAsync(dto.Title, dto.CategoryId);
        return Created();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult>DeleteTodo(Guid id)
    {
        await _todoService.DeleteTodoAsync(id);
        return Ok();
    }
}