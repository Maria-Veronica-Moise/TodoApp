using Todo.Api.DTOs;
using Todo.Api.Models;
using Todo.Api.Repositories;

namespace Todo.Api.Services;

public class TodoService
{
    private readonly TodoRepository _repository;
    private List<TodoItem> _todos = [];

    public TodoService(TodoRepository repository)
    {
        _repository = repository;
    }

    public async Task InitializeAsync()
    {
        _todos = await _repository.LoadAsync();
    }

    public async Task CreateTodoAsync(string title)
    {
        var item = new TodoItem(title);
       
        _todos.Add(item);

        await _repository.SaveAsync(_todos);
    }

    public IEnumerable<TodoDto> GetAll()
    {
        return MapToDto(_todos);
    }

    public async Task CompleteTodoAsync(Guid id)
    {
        var todo = _todos.Single(x => x.Id == id);
     
        todo.MarkAsCompleted();
       
        await _repository.SaveAsync(_todos);
    }
    public async Task DeleteTodoAsync(Guid id)
    {
        var todo = _todos.Single(x => x.Id == id);
        _todos.Remove(todo);
        await _repository.SaveAsync(_todos);
    }
    public IEnumerable<TodoDto> GetCompletedTodos()
    {

        var todos = _todos.Where(todo => todo.Status.Equals(TodoItemStatus.Completed));
        return MapToDto(todos);
    }
    public IEnumerable<TodoDto> GetPendingTodos()
    {
        var todos = _todos.Where(todo => !todo.Status.Equals(TodoItemStatus.New));
        return MapToDto(todos);
    }
    
    public IEnumerable<TodoDto> GetNewestTodos()
    {
        var todos = _todos.OrderByDescending(x => x.CreatedAt);
        return MapToDto(todos);
    }
    public IEnumerable<TodoDto> GetOldestTodos()
    {
        var todos = _todos.OrderBy(x => x.CreatedAt);
        return MapToDto(todos);
    }

    public IEnumerable<TodoDto> GetTodoSummaries()
    {
        return _todos
            .Select((x, i) => new TodoDto
            {
                Id = x.Id,
                Order = i,
                Title = x.Title,
                IsCompleted = x.Status == TodoItemStatus.Completed
            });
    }

    private IEnumerable<TodoDto> MapToDto(IEnumerable<TodoItem> todoItems)
    {
       var todoDtos = todoItems.Select((x, i) => new TodoDto
        {
            Id = x.Id,
            Order = i,
            Title = x.Title,
            IsCompleted = x.Status == TodoItemStatus.Completed
        });
        return todoDtos;
    }
}