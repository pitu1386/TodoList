using TodoList.Shared.Dtos.Progression;

namespace TodoList.Shared.Dtos.TodoItem;

public class TodoItemResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Category { get; set; } = "";
    public List<ProgressionDto> Progressions { get; set; } = new();
}
