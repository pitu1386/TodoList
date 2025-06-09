using TodoList.Domain.Entities;
using TodoList.Shared.Dtos.Progression;
using TodoList.Shared.Dtos.TodoItem;

namespace TodoList.Application.Interfaces;

public interface ITodoList
{
    void AddItem(CreateTodoItemDto dto);
    void UpdateItem(int id, string description);
    void RemoveItem(int id);
    void RegisterProgression(RegisterProgressionDto dto);
    void PrintItems();
    List<TodoItem> GetItems();
}
