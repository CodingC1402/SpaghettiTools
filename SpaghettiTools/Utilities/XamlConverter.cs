using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SpaghettiTools.Utilities
{
    public class IntToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType == typeof(int))
            {
                return ((int)value).ToString();
            }

            return null;
        }


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType == typeof(string))
            {
                return int.Parse((string)value);
            }

            return null;
        }
    }

    public class DoubleToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((double)value).ToString();
        }


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string str = (string)value;
            if (str == "")
                return 1;
            else
                return int.Parse(str);
        }
    }
}
