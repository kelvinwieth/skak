namespace Skak.Discord.Models.Enums
{
    public enum Month
    {
        None,
        January,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December,
    }

    public static class MonthExtension
    {
        private static readonly Dictionary<Month, string> monthNames = new()
        {
            { Month.None, "Nenhum" },
            { Month.January, "Janeiro" },
            { Month.February, "Fevereiro" },
            { Month.March, "Março" },
            { Month.April, "Abril" },
            { Month.May, "Maio" },
            { Month.June, "Junho" },
            { Month.July, "Julho" },
            { Month.August, "Agosto" },
            { Month.September, "Setembro" },
            { Month.November, "November" },
            { Month.December, "Dezembro" },
        };

        public static string ToStringAsPortuguese(this Month month)
        {
            return monthNames[month];
        }

        public static Month ToMonth(this DateTime date)
        {
            var monthInt = date.Month;

            try
            {
                return (Month)monthInt;
            }
            catch
            {
                return Month.None;
            }
        }
    }
}
