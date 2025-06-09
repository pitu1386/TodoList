using TodoList.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TodoList.Test.Services;

public class TodoItemTests
{
    [Fact]
    public void CreateTodoItem_ValidData_ShouldCreateSuccessfully()
    {
        // Arrange & Act
        var todoItem = new TodoItem(1, "Test Title", "Test Description", "Work");

        // Assert
        Assert.Equal(1, todoItem.Id);
        Assert.Equal("Test Title", todoItem.Title);
        Assert.Equal("Test Description", todoItem.Description);
        Assert.Equal("Work", todoItem.Category);
        Assert.False(todoItem.Completed);
        Assert.Equal(0, todoItem.TotalProgress);
    }

    [Theory]
    [InlineData("", "Description", "Work")]
    [InlineData("Title", "", "Work")]
    [InlineData("Title", "Description", "")]
    [InlineData(null, "Description", "Work")]
    [InlineData("Title", null, "Work")]
    [InlineData("Title", "Description", null)]
    public void CreateTodoItem_InvalidData_ShouldThrowException(string title, string description, string category)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new TodoItem(1, title, description, category));
    }

    [Fact]
    public void AddProgression_ValidData_ShouldAddSuccessfully()
    {

        Progression progression = new Progression(DateTime.Now, 30);

        var todoItem = new TodoItem(1, "Test", "Test", "Work");
        // Act
        todoItem.AddProgression(progression);

        // Assert
        Assert.Single(todoItem.Progressions);
        Assert.Equal(30, todoItem.TotalProgress);
        Assert.False(todoItem.Completed);
    }

    [Fact]
    public void AddProgression_100Percent_ShouldMarkAsCompleted()
    {
        // Arrange
        var todoItem = new TodoItem(1, "Test", "Test", "Work");

        // Act
        var progressions = new List<Progression>
            {
                new Progression(DateTime.Now, 30),
                new Progression(DateTime.Now.AddDays(1), 50),
                new Progression(DateTime.Now.AddDays(2), 20)
            };

        foreach (var progression in progressions)
        {
            todoItem.AddProgression(progression);
        }

        // Assert
        Assert.Equal(100, todoItem.TotalProgress);
        Assert.True(todoItem.Completed);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    [InlineData(100)]
    [InlineData(150)]
    public void AddProgression_InvalidPercent_ShouldThrowException(decimal percent)
    {
        // Arrange
        var todoItem = new TodoItem(1, "Test", "Test", "Work");

        // Act & Assert
        Progression progression = new Progression(DateTime.Now, percent);
        Assert.Throws<ArgumentException>(() => todoItem.AddProgression(progression));
    }

    [Fact]
    public void AddProgression_DateNotIncremental_ShouldThrowException()
    {
        // Arrange
        var todoItem = new TodoItem(1, "Test", "Test", "Work");
        var date1 = new DateTime(2025, 3, 20);
        var date2 = new DateTime(2025, 3, 19); // Earlier date

        // Act
        Progression progression = new Progression(date1, 30);
        todoItem.AddProgression(progression);

        // Assert
        Progression progression2 = new Progression(date2, 20);
        Assert.Throws<ArgumentException>(() => todoItem.AddProgression(progression2));
    }

    [Fact]
    public void AddProgression_ExceedsTotal_ShouldThrowException()
    {
        // Arrange
        var todoItem = new TodoItem(1, "Test", "Test", "Work");

        // Act
        Progression progression = new Progression(DateTime.Now, 60);
        todoItem.AddProgression(progression);

        // Assert
        Progression progression1 = new Progression(DateTime.Now.AddDays(1), 50);
        Assert.Throws<ArgumentException>(() => todoItem.AddProgression(progression));
    }

    [Fact]
    public void UpdateDescription_WithLowCompletion_ShouldUpdateSuccessfully()
    {
        // Arrange
        var todoItem = new TodoItem(1, "Test", "Test", "Work");
        Progression progression = new Progression(DateTime.Now, 30);
        todoItem.AddProgression(progression);

        // Act
        todoItem.UpdateDescription("New Description");

        // Assert
        Assert.Equal("New Description", todoItem.Description);
    }

    [Fact]
    public void UpdateDescription_WithHighCompletion_ShouldThrowException()
    {
        // Arrange
        var todoItem = new TodoItem(1, "Test", "Test", "Work");
        Progression progression = new Progression(DateTime.Now, 60);
        todoItem.AddProgression(progression);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => todoItem.UpdateDescription("New Description"));
    }
}
