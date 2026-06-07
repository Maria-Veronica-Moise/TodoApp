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

    public async Task CreateTodoAsync(string title, string assignedTo)
    {
        var item = new TodoItem(title, assignedTo);
       
        _todos.Add(item);

        await _repository.SaveAsync(_todos);
    }

    public IEnumerable<TodoItem> GetAll()
    {
        return _todos;
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
    public IEnumerable<TodoItem> GetCompletedTodos()
    {

        var todos = _todos.Where(todo => todo.Status.Equals(TodoItemStatus.Completed));
        return todos;
    }
    public IEnumerable<TodoItem> GetPendingTodos()
    {
        var todos = _todos.Where(todo => !todo.Status.Equals(TodoItemStatus.New));
        return todos;
    }
    public IEnumerable<TodoItem> GetTodosForUser(string user)
    {
        var todos = _todos.Where(todo => todo.AssignedTo == user);
        return todos;

    }
    public IEnumerable<TodoItem> GetNewestTodos()
    {
        var todos = _todos.OrderByDescending(x => x.CreatedAt);
        return todos;
    }
    public IEnumerable<TodoItem> GetOldestTodos()
    {
        var todos = _todos.OrderBy(x => x.CreatedAt);
        return todos;
    }

    public IEnumerable<TodoSummaryDto> GetTodoSummaries()
    {
        return _todos
            .Select((x, i) => new TodoSummaryDto
            {
                Id = x.Id,
                Order = i,
                Title = x.Title,
                IsCompleted = x.Status == TodoItemStatus.Completed
            });
    }
}