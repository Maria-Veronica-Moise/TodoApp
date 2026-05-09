using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using TodoApp.DTOs;
using TodoApp.Models;
using TodoApp.Repositories;

namespace TodoApp.Services;

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

    public async Task CreateTodoAsync(string title, string description, string assignedTo)
    {
        //validate input
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

        //create a new TodoItem
        var item = new TodoItem
        {
            AssignedTo = assignedTo,
            Title = title,
            Description = description
        };

        //add it to _todos
        _todos.Add(item);

        //save _todos using the repository
        await _repository.SaveAsync(_todos);
    }

    public IEnumerable<TodoItem> GetAll()
    {
        return _todos;
    }

    public async Task CompleteTodoAsync(Guid id)
    {
        //find the todo by id (SingleOrDefault), similar to Where(x => ...)
        var todo = _todos.Single(x => x.Id == id);
        //mark it completed => set IsCompleted to true
        todo.IsCompleted = true;
        //set CompletedAt // DateTime.Now
        todo.CompletedAt = DateTime.Now;
        //save using the repository => same as in create todo
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

        var todos = _todos.Where(todo => todo.IsCompleted);
        return todos;
    }
    public IEnumerable<TodoItem> GetPendingTodos()
    {
        var todos = _todos.Where(todo => !todo.IsCompleted);
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
                Order =  i,
                Title = x.Title,
                IsCompleted = x.IsCompleted
            });
    }
}