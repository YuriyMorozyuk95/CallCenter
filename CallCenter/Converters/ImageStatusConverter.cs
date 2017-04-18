using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace CallCenter.Converters
{
    class ImageStatusConverter : IValueConverter
    {
        readonly Dictionary<bool, BitmapImage> _cache = new Dictionary<bool, BitmapImage>();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var status = (bool) value;

            if (!_cache.ContainsKey(status))
            {
                var uri = status
                    ? new Uri(@"../Resources/free.png", UriKind.Relative)
                    : new Uri(@"../Resources/busy.png", UriKind.Relative);

                _cache.Add(status, new BitmapImage(uri));
            }

            return _cache[status];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
