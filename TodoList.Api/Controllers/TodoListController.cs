using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Interfaces;
using TodoList.Domain.Interfaces;
using TodoList.Shared.Dtos.Progression;
using TodoList.Shared.Dtos.TodoItem;
using static TodoList.Shared.Dtos.TodoItem.TodoItemFormattedDto;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoList _todoList;
        private readonly ITodoListRepository _repository;
        private readonly IMapper _mapper;

        public TodoListController(ITodoList todoList, ITodoListRepository repository, IMapper mapper)
        {
            _todoList = todoList;
            _repository = repository;
            _mapper = mapper;
        }


        [HttpPost]
        public IActionResult Create([FromBody] CreateTodoItemDto dto)
        {
            try
            {
                int id = _repository.GetNextId();
                dto.Id = id;
                _todoList.AddItem(dto);
                return Ok(new { Id = id, Message = "Item created successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] string description)
        {
            try
            {
                _todoList.UpdateItem(id, description);
                return Ok(new { Message = "Item updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("items/{id}")]
        public IActionResult RemoveItem(int id)
        {
            try
            {
                _todoList.RemoveItem(id);
                return Ok(new { Message = "Item removed successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }



        [HttpPost("progression")]
        public IActionResult RegisterProgression([FromBody] RegisterProgressionDto dto)
        {         
            try
            {
                _todoList.RegisterProgression(dto);
                return Ok(new { Message = "Progression registered successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
   
      
        [HttpGet("items")]
        public IActionResult GetItems()
        {
            try
            {

                var result = _todoList.GetItems();
                var dtoList = _mapper.Map<List<TodoItemResponseDto>>(result);
                return Ok(dtoList);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("items/formatted")]
        public IActionResult GetFormattedItems()
        {
            try
            {
                var items = _todoList.GetItems();
                var result = items.OrderBy(x => x.Id).Select(item =>
                {
                    var formattedProgressions = new List<object>();
                    decimal cumulativePercent = 0;

                    foreach (var progression in item.Progressions)
                    {
                        cumulativePercent += progression.Percent;
                        formattedProgressions.Add(new
                        {
                            Date = progression.Date,
                            Percent = progression.Percent,
                            CumulativePercent = cumulativePercent,
                            ProgressBar = item.GetProgressBar(cumulativePercent)
                        });
                    }

                    return new 
                    {
                        Id = item.Id,
                        Title = item.Title,
                        Description = item.Description,
                        Category = item.Category,
                        IsCompleted = item.Completed,
                        TotalProgress = item.TotalProgress,
                        FormattedHeader = $"{item.Id}) {item.Title} - {item.Description} ({item.Category}) Completed:{item.Completed}",
                        Progressions = formattedProgressions
                    };
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("seed")]
        public IActionResult SeedTestData()
        {
            try
            {
                var categories = _repository.GetAllCategories();
                var rnd = new Random();
                var baseDate = DateTime.Today.AddDays(-10); // fecha inicial base

                for (int i = 1; i <= 5; i++)
                {
                    var id = _repository.GetNextId();

                    var dto = new CreateTodoItemDto
                    {
                        Id = id,
                        Title = $"Tarea {i}",
                        Description = $"Descripción auto-generada {i}",
                        Category = categories[rnd.Next(categories.Count)]
                    };

                    _todoList.AddItem(dto);

                    var numProgress = rnd.Next(1, 4);
                    for (int j = 0; j < numProgress; j++)
                    {
                        var prog = new RegisterProgressionDto
                        {
                            TodoItemId = id,
                            Date = baseDate.AddDays(i * 5 + j), // garantiza fechas crecientes por item
                            Percent = Math.Round((decimal)(rnd.NextDouble() * 100), 2)
                        };

                        _todoList.RegisterProgression(prog);
                    }
                }

                return Ok(new { Message = "Se cargaron datos de prueba correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }



    }
}
