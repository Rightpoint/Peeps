using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Microsoft.ProjectOxford.Face.Contract;

namespace Rightpoint.Peeps.Client.Converters
{
    public class FaceLandmarkToHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null || !(value is FaceLandmarks))
                return null;

            var landmarks = (FaceLandmarks) value;
            return landmarks.UnderLipBottom.Y - landmarks.NoseTip.Y;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
