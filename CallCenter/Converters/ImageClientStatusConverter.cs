using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace CallCenter.Converters
{
    class ImageClientStatusConverter : IValueConverter
    {
        readonly Dictionary<ClientStatus, BitmapImage> _cache = new Dictionary<ClientStatus, BitmapImage>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var clientStatus = (ClientStatus)value;

            if (!_cache.ContainsKey(clientStatus))
            {
                var uri = new Uri(@"", UriKind.Relative);

                switch (clientStatus)
                {
                    case ClientStatus.Connected :
                        uri = new Uri(@"../Resources/phone.png", UriKind.Relative);
                        break;
                    case ClientStatus.Wating :
                        uri = new Uri(@"../Resources/hourglass.png", UriKind.Relative);
                        break;
                    case ClientStatus.Missed :
                        uri = new Uri(@"../Resources/missed.png", UriKind.Relative);
                        break;
                }

                _cache.Add(clientStatus, new BitmapImage(uri));
            }

            return _cache[clientStatus];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
