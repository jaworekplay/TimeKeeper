using System.Globalization;
using System.Windows.Data;

namespace TimeKeeper.Converters
{
    public class TodayToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s)
            {
                var now = DateTime.Now;
                var sNow = $"{now.Day} {now:MMMM}, {now.DayOfWeek}";
                return s == sNow;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
