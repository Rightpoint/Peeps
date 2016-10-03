using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Data;
using Rightpoint.Peeps.Client.Models;

namespace Rightpoint.Peeps.Client.Converters
{
    public class CountZeroValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null || !(value is ObservableCollection<Peep>))
                return true;

            return (value as ObservableCollection<Peep>).Count == 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
