using AutoMapper;
using TodoList.Domain.Entities;
using TodoList.Shared.Dtos.TodoItem;

namespace TodoList.Application.Mappers;

public class TodoItemMapper : Profile
{
    public TodoItemMapper()
    {
        CreateMap<TodoItem, CreateTodoItemDto>().ReverseMap();
        CreateMap<TodoItem, TodoItemResponseDto>();
    }
}

