using AutoMapper;
using TodoList.Domain.Entities;
using TodoList.Shared.Dtos.Progression;
using TodoList.Shared.Dtos.TodoItem;

namespace TodoList.Application.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<TodoItem, CreateTodoItemDto>().ReverseMap();
        CreateMap<Progression, RegisterProgressionDto>().ReverseMap();
    }
}