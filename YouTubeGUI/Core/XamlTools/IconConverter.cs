using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace YouTubeGUI.Core.XamlTools
{
    public class IconConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                switch (value)
                {
                    case "WATCH_LATER":
                        if (App.Current.Styles.TryGetResource("ClockRegular", out object? wlParam))
                            return wlParam!;
                        break;
                    case "CHECK":
                        if (App.Current.Styles.TryGetResource("CheckmarkRegular", out object? checkIcon))
                            return checkIcon!;
                        break;
                    case "ADD_TO_QUEUE_TAIL":
                        if (App.Current.Styles.TryGetResource("BulletListAdd", out object? atqIcon))
                            return atqIcon!;
                        break;
                    case "PLAYLIST_ADD_CHECK":
                        if (App.Current.Styles.TryGetResource("BulletList", out object? pacIcon))
                            return pacIcon!;
                        break;
                    case "MIX":
                        if (App.Current.Styles.TryGetResource("LiveRegular", out object? mixIcon))
                            return mixIcon!;
                        break;
                    case "PLAY_ALL":
                        if (App.Current.Styles.TryGetResource("PlayRegular", out object? paIcon))
                            return paIcon!;
                        break;
                }
            }
            if (App.Current.Styles.TryGetResource("WarningRegular", out object? defIcon))
                return defIcon!;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}