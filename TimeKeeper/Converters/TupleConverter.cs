using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TimeKeeper.Converters
{
    public class TupleConverter : IValueConverter
    {
        public int PropertyIndex { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ValueTuple<uint,TimeSpan,DateTime> vt)
            {
                if (PropertyIndex == 0)
                {
                    return vt.Item1;
                }
                else if (PropertyIndex == 1)
                {
                    return vt.Item2;
                }
                else
                {
                    return vt.Item3;
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
