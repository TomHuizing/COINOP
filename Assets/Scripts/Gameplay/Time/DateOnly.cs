using System;
using UnityEngine;

public readonly struct DateOnly
{
    public int Year { get; }
    public int Month { get; }
    public int Day { get; }

    public DateOnly(int year, int month, int day)
    {
        if (year < 0)
            throw new System.ArgumentOutOfRangeException(nameof(year), "Year must be a non-negative integer.");
        if (month < 1 || month > 12)
            throw new System.ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12.");
        if (day < 1 || day > DateTime.DaysInMonth(year, month))
            throw new System.ArgumentOutOfRangeException(nameof(day), $"Day must be between 1 and {DateTime.DaysInMonth(year, month)} for the given month and year.");

        Year = year;
        Month = month;
        Day = day;
    }

    public override string ToString()
    {
        return $"{Year:D4}-{Month:D2}-{Day:D2}";
    }

    public DateOnly AddDays(int days)
    {
        DateTime dateTime = new DateTime(Year, Month, Day).AddDays(days);
        return new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
    }

    public DateOnly AddMonths(int months)
    {
        DateTime dateTime = new DateTime(Year, Month, Day).AddMonths(months);
        return new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
    }

    public DateOnly AddYears(int years)
    {
        DateTime dateTime = new DateTime(Year, Month, Day).AddYears(years);
        return new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
    }

    public DateTime ToDateTime(TimeOnly time)
    {
        return new DateTime(Year, Month, Day, time.Hour, time.Minute, 0);
    }

    public static DateOnly operator +(DateOnly a, TimeSpan b)
    {
        DateTime dateTime = new DateTime(a.Year, a.Month, a.Day).Add(b);
        return new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
    }

    public static DateOnly operator -(DateOnly a, TimeSpan b)
    {
        DateTime dateTime = new DateTime(a.Year, a.Month, a.Day).Subtract(b);
        return new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
    }
}
