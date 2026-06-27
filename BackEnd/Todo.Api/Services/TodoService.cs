using Todo.Api.DTOs;
using Todo.Api.Models;
using Todo.Api.Repositories;

namespace Todo.Api.Services;

public class TodoService
{
    private readonly TodoRepository _repository;
    private readonly CategoryRepository _categoryRepository;

    public TodoService(
        TodoRepository repository,
        CategoryRepository categoryRepository)
    {
        _repository = repository;
        _categoryRepository = categoryRepository;
    }


    public async Task CreateTodoAsync(string title, Guid categoryId)
    {
        var item = new TodoItem(title,categoryId);
        var todos = await _repository.LoadAsync();
        todos.Add(item);

        await _repository.SaveAsync(todos);
    }

    public async Task<IEnumerable<TodoDto>> GetAll()
    {
        var todos = await _repository.LoadAsync();

        return await MapToDto(todos);
    }

    public async Task StatusTodoAsync(Guid id)
    {
        var todos = await _repository.LoadAsync();
        var todo = todos.Single(x => x.Id == id);

        Console.WriteLine("Current status: {todo.Status}");

        if (todo.Status == TodoItemStatus.Completed)
        {
            todo.MarkAsPending();
            Console.WriteLine("pending");
        }
        else 
        {
            todo.MarkAsCompleted();
            Console.WriteLine("completed");
        }

        await _repository.SaveAsync(todos);
    }
    public async Task DeleteTodoAsync(Guid id)
    {
        var todos = await _repository.LoadAsync();

        var todo = todos.Single(x => x.Id == id);
        todos.Remove(todo);
        await _repository.SaveAsync(todos);
    }
    public async Task<IEnumerable<TodoDto>> GetCompletedTodos()
    {
        var todos = await _repository.LoadAsync();
        todos = todos.Where(todo => todo.Status.Equals(TodoItemStatus.Completed)).ToList();
        return await MapToDto(todos);
    }
    public async Task<IEnumerable<TodoDto>> GetPendingTodos()
    {
        var todos = await _repository.LoadAsync();

        todos = todos.Where(todo => !todo.Status.Equals(TodoItemStatus.Completed)).ToList();
        return await MapToDto(todos);
    }

    public async Task<IEnumerable<TodoDto>> GetNewestTodos()
    {
        var todos = await _repository.LoadAsync();

        todos = todos.OrderByDescending(x => x.CreatedAt).ToList();
        return await MapToDto(todos);
    }
    public async Task<IEnumerable<TodoDto>> GetOldestTodos()
    {
        var todos = await _repository.LoadAsync();

        todos = todos.OrderBy(x => x.CreatedAt).ToList();
        return await MapToDto(todos);
    }

    public async Task<IEnumerable<TodoDto>> GetTodoSummaries()
    {
        var todos = await _repository.LoadAsync();

        return todos
            .Select((x, i) => new TodoDto
            {
                Id = x.Id,
                Order = i,
                Title = x.Title,
                IsCompleted = x.Status == TodoItemStatus.Completed
            });
    }

    private async Task<IEnumerable<TodoDto>> MapToDto(IEnumerable<TodoItem> todoItems)
    {
        var categories = await _categoryRepository.LoadAsync();

        var todoDtos = todoItems.Select((x, i) => new TodoDto
        {
            Id = x.Id,
            Order = i,
            Title = x.Title,
            CategoryId = x.CategoryId,
            CategoryName = categories
                .SingleOrDefault(c => c.Id == x.CategoryId)
                .Name?? "Unknown",
            IsCompleted = x.Status == TodoItemStatus.Completed
        });

        return todoDtos;
    }
}