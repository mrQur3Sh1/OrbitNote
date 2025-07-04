using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using NotebookMVVM.Business.Model;

namespace NotebookMVVM.Ui.Desktop.Converters
{
    public class NoteStateToDraftVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is NoteState state && state == NoteState.Draft ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
