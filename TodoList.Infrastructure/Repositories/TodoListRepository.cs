using TodoList.Domain.Interfaces;

namespace TodoList.Infrastructure.Repositories;

public class TodoListRepository : ITodoListRepository
{
    private readonly List<string> _categories = new() { "Work", "Personal", "Hobby" };
    private int _currentId = 0;

    public int GetNextId() => ++_currentId;

    public List<string> GetAllCategories() => _categories;
}
