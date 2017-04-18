using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace CallCenter.Converters
{
    class ImageSexConverter : IValueConverter
    {
        readonly Dictionary<Sex, BitmapImage> _cache = new Dictionary<Sex, BitmapImage>();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        { 
            var sex = (Sex) value;

            if (!_cache.ContainsKey(sex))
            {
                var uri = sex == Sex.Male
                    ? new Uri(@"../Resources/supportmale.png", UriKind.Relative)
                    : new Uri(@"../Resources/supportfemale.png", UriKind.Relative);

                _cache.Add(sex, new BitmapImage(uri));
            }

            return _cache[sex];
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
