
public static class CalculateTime
{
    public static int calculateDowntimeHours(DateTime? startDate, DateTime? endDate)
    {
        if (startDate is DateTime start && endDate is DateTime end && end >= start)
        {
            return (int)(end - start).TotalHours;
        }
        return 0; 
    }
}

