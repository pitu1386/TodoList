using AutoMapper;
using TodoList.Application.Interfaces;
using TodoList.Domain.Interfaces;
using TodoList.Domain.Entities;
using TodoList.Shared.Dtos.Progression;
using TodoList.Shared.Dtos.TodoItem;

namespace TodoList.Application.Services
{
    public class TodoList : ITodoList
    {
        private readonly ITodoListRepository _repository;
        private readonly IMapper _mapper;
        private readonly List<TodoItem> _items = new();

        public TodoList(ITodoListRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void AddItem(CreateTodoItemDto dto)
        {
            if (!_repository.GetAllCategories().Contains(dto.Category))
                throw new ArgumentException("Invalid category");

            var item = _mapper.Map<TodoItem>(dto);
            _items.Add(item);
        }

        public void UpdateItem(int id, string description)
        {
            var item = _items.FirstOrDefault(x => x.Id == id)
                ?? throw new ArgumentException("Item not found");
            item.UpdateDescription(description);
        }

        public void RemoveItem(int id)
        {
            var item = _items.FirstOrDefault(x => x.Id == id) ?? throw new ArgumentException("Item not found");

            if (item.IsMoreHalfCompleted())
                throw new InvalidOperationException("Cannot remove an item over 50% completed");

            _items.Remove(item);
        }

        public void RegisterProgression(RegisterProgressionDto dto)
        {
            var item = _items.FirstOrDefault(x => x.Id == dto.TodoItemId) ?? throw new ArgumentException("Item not found");
            var progre = _mapper.Map<Progression>(dto);
            item.AddProgression(progre);
        }


        public void PrintItems()
        {
            foreach (var item in _items.OrderBy(x => x.Id))
            {
                Console.WriteLine($"{item.Id}) {item.Title} - {item.Description} ({item.Category}) Completed:{item.Completed}");
                decimal accumulated = 0;
                foreach (var p in item.Progressions)
                {
                    accumulated += p.Percent;
                    int barLength = (int)(accumulated);
                    Console.WriteLine($"{p.Date} - {accumulated}% |{new string('O', barLength)} |");
                }
            }
        }
        public List<TodoItem> GetItems()
        {
            return _items;
        }

    
    }
}
