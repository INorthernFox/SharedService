namespace SharedKernel.Intervals;

public static class IntervalTypeExtensions
{
    public static TimeSpan TotDelay(this IntervalType intervalType, string? timeString)
    {
        if (!TimeSpan.TryParse(timeString, out TimeSpan time))
            time = TimeSpan.Zero;

        if (time < TimeSpan.Zero || time >= TimeSpan.FromDays(1))
            throw new ArgumentOutOfRangeException(nameof(time), "Должно быть в пределах 00:00..23:59:59.999");

        var now = DateTime.UtcNow;

        DateTime next;

        switch (intervalType)
        {
            case IntervalType.DEV:
                return TimeSpan.FromSeconds(10);
            case IntervalType.HOURLY:
                {
                    next = new DateTime(now.Year, now.Month, now.Day, now.Hour,
                            time.Minutes, time.Seconds, now.Kind)
                        .AddMilliseconds(time.Milliseconds)
                        .AddHours(1);
                    break;
                }
            case IntervalType.DAILY:
                {
                    var todayAt = now.Date + time;
                    next = todayAt > now ? todayAt : todayAt.AddDays(1);
                    break;
                }
            case IntervalType.WEEKLY:
                {
                    var anchor = now.Date + time;
                    next = anchor.AddDays(7);
                    break;
                }
            case IntervalType.MONTHLY:
                {
                    var anchor = now.Date + time;
                    next = AddMonthsSafe(anchor, 1);
                    break;
                }
            case IntervalType.YEARLY:
                {
                    var anchor = now.Date + time;
                    next = AddYearsSafe(anchor, 1);
                    break;
                }
            default:
                throw new ArgumentOutOfRangeException(nameof(intervalType), intervalType, null);
        }

        return next - now;
    }

    private static DateTime AddMonthsSafe(DateTime dt, int months)
    {
        int total = (dt.Year * 12) + (dt.Month - 1) + months;
        int y = total / 12;
        int m = (total % 12) + 1;
        int day = Math.Min(dt.Day, DateTime.DaysInMonth(y, m));
        return new DateTime(y, m, day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond, dt.Kind);
    }

    private static DateTime AddYearsSafe(DateTime dt, int years)
    {
        int y = dt.Year + years;
        int day = dt.Day;
        if (dt.Month == 2)
            day = Math.Min(day, DateTime.DaysInMonth(y, 2));
        return new DateTime(y, dt.Month, day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond, dt.Kind);
    }
}