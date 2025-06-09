using Xunit;
using Moq;
using AutoMapper;
using TodoList.Domain.Interfaces;
using TodoList.Application.Mappers;
using TodoList.Shared.Dtos.TodoItem;
using TodoListService = TodoList.Application.Services;
using TodoList.Shared.Dtos.Progression;

namespace TodoList.Test.Services;

public class TodoListTests
{
    private readonly Mock<ITodoListRepository> _repositoryMock;
    private readonly IMapper _mapper;
    private readonly TodoListService.TodoList _todoList;

    public TodoListTests()
    {
        _repositoryMock = new Mock<ITodoListRepository>();
        _repositoryMock.Setup(r => r.GetAllCategories()).Returns(new List<string> { "Work", "Personal" });

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<TodoItemMapper>();
            cfg.AddProfile<ProgressionMapper>();
        });

        _mapper = config.CreateMapper();
        _todoList = new TodoListService.TodoList(_repositoryMock.Object, _mapper);
    }

    [Fact]
    public void AddItem_ShouldAddValidItem()
    {
        var dto = new CreateTodoItemDto { Id = 1, Title = "Task", Description = "Desc", Category = "Work" };
        _todoList.AddItem(dto);
        _todoList.PrintItems(); 
    }

    [Fact]
    public void AddItem_ShouldThrow_WhenCategoryIsInvalid()
    {
        var dto = new CreateTodoItemDto { Id = 1, Title = "Task", Description = "Desc", Category = "Invalid" };
        Assert.Throws<ArgumentException>(() => _todoList.AddItem(dto));
    }

    [Fact]
    public void UpdateItem_ShouldUpdateDescription()
    {
        var dto = new CreateTodoItemDto { Id = 1, Title = "Task", Description = "Old", Category = "Work" };
        _todoList.AddItem(dto);

        _todoList.UpdateItem(1, "New");
    }

    [Fact]
    public void RemoveItem_ShouldRemoveItem()
    {
        var dto = new CreateTodoItemDto { Id = 1, Title = "Task", Description = "Desc", Category = "Work" };
        _todoList.AddItem(dto);
        _todoList.RemoveItem(1);
    }

    [Fact]
    public void RemoveItem_ShouldThrow_WhenOver50Percent()
    {
        var dto = new CreateTodoItemDto { Id = 1, Title = "Task", Description = "Desc", Category = "Work" };
        _todoList.AddItem(dto);
        var dtoRP = new RegisterProgressionDto
        {
            TodoItemId = 1,
            Date = DateTime.Now,
            Percent = 30
        };

        _todoList.RegisterProgression(dtoRP);

        Assert.Throws<InvalidOperationException>(() => _todoList.RemoveItem(1));
    }

    [Fact]
    public void RegisterProgression_ShouldAddValidProgression()
    {
        var dto = new CreateTodoItemDto { Id = 1, Title = "Task", Description = "Desc", Category = "Work" };
        _todoList.AddItem(dto);

        var dtoRP = new RegisterProgressionDto
        {
            TodoItemId = 1,
            Date = DateTime.Now,
            Percent = 30
        };

        _todoList.RegisterProgression(dtoRP);
    }

    [Fact]
    public void RegisterProgression_ShouldThrow_WhenOver100Percent()
    {
        var dto = new CreateTodoItemDto { Id = 1, Title = "Task", Description = "Desc", Category = "Work" };
        _todoList.AddItem(dto);
        var dtoRP = new RegisterProgressionDto
        {
            TodoItemId = 1,
            Date = DateTime.Now,
            Percent = 90
        };

        _todoList.RegisterProgression(dtoRP);

        var dtoRPnew = new RegisterProgressionDto
        {
            TodoItemId = 1,
            Date = DateTime.Today.AddDays(1),
            Percent = 20
        };

        _todoList.RegisterProgression(dtoRP);

        Assert.Throws<InvalidOperationException>(() => _todoList.RegisterProgression(dtoRPnew));
    }

    [Fact]
    public void PrintItems_ShouldPrintFormattedOutput()
    {
        var dto = new CreateTodoItemDto { Id = 1, Title = "Task", Description = "Desc", Category = "Work" };
        _todoList.AddItem(dto);
        var dtoRP = new RegisterProgressionDto
        {
            TodoItemId = 1,
            Date = DateTime.Now,
            Percent = 90
        };

        _todoList.RegisterProgression(dtoRP);


        using var sw = new StringWriter();
        Console.SetOut(sw);

        _todoList.PrintItems();
        var output = sw.ToString();

        Assert.Contains("1) Task - Desc (Work) Completed:False", output);
        Assert.Contains("30%", output);
    }
}
