namespace Mepas.Sub.API.Helpers
{
    public static class DateHelper
    {
        // Tarih aralığının geçerliliğini kontrol eder
        public static bool IsValidDateRange(DateTime? startDate, DateTime? endDate)
        {
            // Eğer başlangıç veya bitiş tarihi boşsa geçerli kabul edilir
            if (!startDate.HasValue || !endDate.HasValue)
            {
                return true;
            }
            return startDate <= endDate;
        }

        // Tarihleri parse eder
        public static (DateTime? StartDate, DateTime? EndDate) ParseDateRange(string startDate, string endDate)
        {
            DateTime? start = string.IsNullOrEmpty(startDate) ? null : DateTime.Parse(startDate);
            DateTime? end = string.IsNullOrEmpty(endDate) ? null : DateTime.Parse(endDate);

            return (start, end);
        }
    }
}
