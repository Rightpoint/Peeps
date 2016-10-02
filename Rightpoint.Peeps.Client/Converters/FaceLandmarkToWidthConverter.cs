using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Microsoft.ProjectOxford.Face.Contract;

namespace Rightpoint.Peeps.Client.Converters
{
    public class FaceLandmarkToWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null || !(value is FaceLandmarks))
                return null;

            var landmarks = (FaceLandmarks) value;

            return landmarks.MouthRight.X - landmarks.MouthLeft.X;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
