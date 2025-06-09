using AutoMapper;
using TodoList.Domain.Entities;
using TodoList.Shared.Dtos.Progression;

namespace TodoList.Application.Mappers;

public class ProgressionMapper : Profile
{
    public ProgressionMapper()
    {
        CreateMap<Progression, RegisterProgressionDto>().ReverseMap();
        CreateMap<Progression, ProgressionDto>();
    }
}
