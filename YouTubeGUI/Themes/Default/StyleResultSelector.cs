using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Styling;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Renderers;

namespace YouTubeGUI.Themes.Default
{
    public class StyleResultSelector : IValueConverter
    {
        public Style VideoStyle { get; set; }
        public Style RadioStyle { get; set; }
        public Style DisplayAdStyle { get; set; }
        public Style RichItemStyle { get; set; }
        public Style RichSectionStyle { get; set; }
        public Style ContinuationStyle { get; set; }
        public Style ChipCloudChipStyle { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case RichItemRenderer rir:
                    if (rir.RichItemContent.VideoRenderer != null)
                        return VideoStyle;
                    if (rir.RichItemContent.RadioRenderer != null)
                        return RadioStyle;
                    if (rir.RichItemContent.DisplayAdRenderer != null)
                        return DisplayAdStyle;
                    return null;
                case RichSectionRenderer:
                    return RichSectionStyle;
                case ContinuationItemRenderer:
                    return ContinuationStyle;
                case ChipCloudChipRenderer:
                    return ChipCloudChipStyle;
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}