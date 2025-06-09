﻿namespace TodoList.Domain.Entities;

public class Progression
{
    public DateTime Date { get; }
    public decimal Percent { get; }

    public Progression(DateTime date, decimal percent)
    {
        Date = date;
        Percent = percent;
    }
}
