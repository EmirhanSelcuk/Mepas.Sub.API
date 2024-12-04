public static class DateHelper
{
    public static (DateTime, DateTime) ParseDateRange(string startDate, string endDate)
    {
        DateTime start = DateTime.Parse(startDate);
        DateTime end = DateTime.Parse(endDate);
        return (start, end);
    }

    public static bool IsValidDateRange(DateTime startDate, DateTime endDate)
    {
        return startDate <= endDate;
    }
}
