using System;

public readonly struct TimeOnly
{
    public int Hour { get; }
    public int Minute { get; }
    public int Second { get; }

    public TimeOnly(int hour, int minute, int second = 0)
    {
        if (hour < 0 || hour >= 24)
            throw new System.ArgumentOutOfRangeException(nameof(hour), "Hours must be between 0 and 23.");
        if (minute < 0 || minute >= 60)
            throw new System.ArgumentOutOfRangeException(nameof(minute), "Minutes must be between 0 and 59.");
        if (second < 0 || second >= 60)
            throw new System.ArgumentOutOfRangeException(nameof(second), "Seconds must be between 0 and 59.");
        Hour = hour;
        Minute = minute;
        Second = second;
    }

    public override string ToString()
    {
        return $"{Hour:D2}:{Minute:D2}";
    }

    public TimeOnly AddSeconds(int seconds)
    {
        int totalSeconds = Hour * 3600 + Minute * 60 + Second + seconds;
        return new TimeOnly(totalSeconds / 3600 % 24, (totalSeconds / 60) % 60, totalSeconds % 60);
    }

    public TimeOnly AddMinutes(int minutes)
    {
        int totalMinutes = Hour * 60 + Minute + minutes;
        return new TimeOnly(totalMinutes / 60 % 24, totalMinutes % 60);
    }

    public TimeOnly AddHours(int hours)
    {
        int totalHours = (Hour + hours) % 24;
        return new TimeOnly(totalHours, Minute);
    }

    public DateTime ToDateTime(DateOnly date)
    {
        return new DateTime(date.Year, date.Month, date.Day, Hour, Minute, 0);
    }

    public static TimeOnly operator -(TimeOnly a, TimeSpan b)
    {
        int totalMinutes = a.Hour * 60 + a.Minute - (int)b.TotalMinutes;
        return new TimeOnly(totalMinutes / 60 % 24, totalMinutes % 60);
    }

    public static TimeOnly operator +(TimeOnly a, TimeSpan b)
    {
        int totalMinutes = a.Hour * 60 + a.Minute + (int)b.TotalMinutes;
        return new TimeOnly(totalMinutes / 60 % 24, totalMinutes % 60);
    }
}
