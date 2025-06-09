using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Shared.Dtos.TodoItem;

public class TodoItemFormattedDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public bool IsCompleted { get; set; }
    public decimal TotalProgress { get; set; }
    public List<FormattedProgressionDto> Progressions { get; set; }

    public class FormattedProgressionDto
    {
        public DateTime Date { get; set; }
        public decimal Percent { get; set; }
        public decimal CumulativePercent { get; set; }
        public string ProgressBar { get; set; }
    }

   
}

