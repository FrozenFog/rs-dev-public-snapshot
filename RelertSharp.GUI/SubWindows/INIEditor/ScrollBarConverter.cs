using System;
using System.Globalization;
using System.Windows.Data;

namespace RelertSharp.SubWindows.INIEditor
{
    public class ScrollBarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)((double)value - 30);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
