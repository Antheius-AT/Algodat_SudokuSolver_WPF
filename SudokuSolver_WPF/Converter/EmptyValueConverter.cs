using System;
using System.Globalization;
using System.Windows.Data;

namespace SudokuSolver_WPF.Converter
{
    public class EmptyValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!int.TryParse(value.ToString(), out int result))
                throw new ArgumentException(nameof(value), "Value must be convertible to int");

            if (result == 0)
                return string.Empty;
            else
                return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
