using TodoList.Domain.Entities;

namespace TodoList.Test.Services;

public class ProgressionTests
{
    [Fact]
    public void CreateProgression_ValidData_ShouldCreateSuccessfully()
    {
        // Arrange
        var date = new DateTime(2025, 3, 18);
        decimal percent = 30;

        // Act
        var progression = new Progression(date, percent);

        // Assert
        Assert.Equal(date, progression.Date);
        Assert.Equal(percent, progression.Percent);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    [InlineData(100)]
    [InlineData(150)]
    public void CreateProgression_InvalidPercent_ShouldThrowException(decimal percent)
    {
        // Arrange
        var date = DateTime.Now;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Progression(date, percent));
    }
}