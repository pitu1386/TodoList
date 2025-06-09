namespace TodoList.Domain.Entities;

public class TodoItem
{
    public int Id { get; }
    public string Title { get; }
    public string Description { get; private set; }
    public string Category { get; }
    private readonly List<Progression> _progressions;
    public IReadOnlyList<Progression> Progressions => _progressions.AsReadOnly();
    public bool Completed { get; private set; }

    public decimal TotalProgress => _progressions.Sum(p => p.Percent);

 

    public TodoItem(int id, string title, string description, string category)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be null or empty");
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be null or empty");
        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Category cannot be null or empty");

        Id = id;
        Title = title;
        Description = description;
        Category = category;
        _progressions = new List<Progression>();
    }

    public void UpdateDescription(string description)
    {
        if (IsMoreHalfCompleted())
            throw new InvalidOperationException("Cannot update an item over 50% completed");
        Description = description;
    }

    public void AddProgression(Progression progression)
    {
        if (_progressions.Count > 0 && progression.Date <= _progressions.Last().Date)
            throw new ArgumentException("Progression date must be greater than previous progression dates.");

        if (progression.Percent <= 0 || progression.Percent > 100)
            throw new ArgumentException("Progression percent must be greater than 0 and less or equal to 100.");

        if (TotalProgress + progression.Percent > 100)
            throw new InvalidOperationException("Total progression cannot exceed 100%.");

        _progressions.Add(progression);
        Completed = TotalProgress >= 100;
    }

    public string GetProgressBar(decimal cumulativePercent)
    {
        int barLength = 50;
        int filledLength = (int)(cumulativePercent / 100 * barLength);
        return "|" + new string('O', filledLength) + new string(' ', barLength - filledLength) + "|";
    }


    public bool IsMoreHalfCompleted() => _progressions.Sum(p => p.Percent) > 50;

}
