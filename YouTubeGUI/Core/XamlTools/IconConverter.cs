using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using YouTubeScrap.Data.Extend;

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
                        if (App.Current.Styles.TryGetResource("BulletListAdd", out object? atqtIcon))
                            return atqtIcon!;
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
                    case "PLAYLISTS":
                        if (App.Current.Styles.TryGetResource("ListRegular", out object? plIcon))
                            return plIcon!;
                        break;
                    case "EXPAND":
                        if (App.Current.Styles.TryGetResource("ChevronDownRegular", out object? exIcon))
                            return exIcon!;
                        break;
                    case "OFFLINE_DOWNLOAD":
                        if (App.Current.Styles.TryGetResource("ArrowDownloadRegular", out object? odIcon))
                            return odIcon!;
                        break;
                    case "MY_VIDEOS":
                        if (App.Current.Styles.TryGetResource("VideoClipRegular", out object? mvIcon))
                            return mvIcon!;
                        break;
                    case "WATCH_HISTORY":
                        if (App.Current.Styles.TryGetResource("ArrowRotateCounterclockwiseRegular", out object? whIcon))
                            return whIcon!;
                        break;
                    case "SUBSCRIPTIONS":
                        if (App.Current.Styles.TryGetResource("LibraryRegular", out object? subIcon))
                            return subIcon!;
                        break;
                    case "TAB_EXPLORE":
                        if (App.Current.Styles.TryGetResource("CompassNorthwestRegular", out object? teIcon))
                            return teIcon!;
                        break;
                    case "WHAT_TO_WATCH":
                        if (App.Current.Styles.TryGetResource("HomeRegular", out object? wtwIcon))
                            return wtwIcon!;
                        break;
                    case "UNLIMITED":
                        if (App.Current.Styles.TryGetResource("MoneyRegular", out object? unIcon))
                            return unIcon!;
                        break;
                    case "MOVIES":
                        if (App.Current.Styles.TryGetResource("MoviesAndTvRegular", out object? movIcon))
                            return movIcon!;
                        break;
                    case "GAMING_LOGO":
                        if (App.Current.Styles.TryGetResource("GamesRegular", out object? gameIcon))
                            return gameIcon!;
                        break;
                    case "LIVE":
                        if (App.Current.Styles.TryGetResource("LiveRegular", out object? liveIcon))
                            return liveIcon!;
                        break;
                    case "TROPHY":
                        if (App.Current.Styles.TryGetResource("TrophyRegular", out object? trophyIcon))
                            return trophyIcon!;
                        break;
                    case "SETTINGS":
                        if (App.Current.Styles.TryGetResource("SettingsRegular", out object? settingsIcon))
                            return settingsIcon!;
                        break;
                    case "FLAG":
                        if (App.Current.Styles.TryGetResource("FlagRegular", out object? flagIcon))
                            return flagIcon!;
                        break;
                    case "HELP":
                        if (App.Current.Styles.TryGetResource("QuestionCircleRegular", out object? helpIcon))
                            return helpIcon!;
                        break;
                    case "FEEDBACK":
                        if (App.Current.Styles.TryGetResource("InfoRegular", out object? feedbackIcon))
                            return feedbackIcon!;
                        break;
                    case "VIDEO_LIBRARY_WHITE":
                        if (App.Current.Styles.TryGetResource("LibraryRegular", out object? vlwIcon))
                            return vlwIcon!;
                        break;
                }
            }
            if (value is GuideBadge gBadge)
            {
                if (gBadge.LiveBroadcasting)
                    if (App.Current.Styles.TryGetResource("LiveRegular", out object? liveIcon))
                        return liveIcon;
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