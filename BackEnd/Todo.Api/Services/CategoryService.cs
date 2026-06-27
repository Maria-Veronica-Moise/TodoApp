using Todo.Api.DTOs;
using Todo.Api.Models;
using Todo.Api.Repositories;

namespace Todo.Api.Services;

public class CategoryService
{
    private readonly CategoryRepository _repository;

    public CategoryService(CategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateCategoryAsync(string name)
    {
        var category = new Category(name);

        var categories = await _repository.LoadAsync();
        categories.Add(category);

        await _repository.SaveAsync(categories);
    }

    public async Task<IEnumerable<CategoryDto>> GetAll()
    {
        var categories = await _repository.LoadAsync();

        return MapToDto(categories);
    }

    private IEnumerable<CategoryDto> MapToDto(IEnumerable<Category> categories)
    {
        return categories.Select(category => new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        });
    }
}