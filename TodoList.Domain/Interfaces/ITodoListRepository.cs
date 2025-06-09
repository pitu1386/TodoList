namespace TodoList.Domain.Interfaces;

public interface ITodoListRepository
{
    int GetNextId();
    List<string> GetAllCategories();
}
