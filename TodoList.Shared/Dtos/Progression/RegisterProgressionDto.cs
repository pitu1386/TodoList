namespace TodoList.Shared.Dtos.Progression;

public class RegisterProgressionDto
{
    public int TodoItemId { get; set; }
    public DateTime Date { get; set; }
    public decimal Percent { get; set; }
}
