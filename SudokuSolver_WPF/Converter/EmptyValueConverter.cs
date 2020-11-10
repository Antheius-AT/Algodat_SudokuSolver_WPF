using System;
using System.Globalization;
using System.Windows.Data;

namespace SudokuSolver_WPF.Converter
{
    /// <summary>
    /// Valut converter which is used to substitute zeroes with empty strings when displayed to the user.
    /// </summary>
    public class EmptyValueConverter : IValueConverter
    {
        /// <summary>
        /// Converts zeroes to empty strings.
        /// </summary>
        /// <param name="value">The incoming cell value.</param>
        /// <param name="targetType">Unused parameter.</param>
        /// <param name="parameter">Unused parameter.</param>
        /// <param name="culture">Unused parameter.</param>
        /// <returns>Either the original value, or an empty string, depending on the cells content.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the incoming value was not an integer.
        /// </exception>
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
