using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using YouTubeGUI.Controls;
using YouTubeScrap.Data.Extend;
using YouTubeScrap.Data.Renderers;

namespace YouTubeGUI.Core.XamlTools
{
    public class BoolCompareVisibilityOverlayConverter : IMultiValueConverter
    {
        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = false;
            bool overlayBool = false;
            if (values.Count < 2)
                if (parameter is string bValueStr) boolValue = bool.Parse(bValueStr);
                else return false;
            int count = 0;
            
            foreach (object value in values)
            {
                switch (value)
                {
                    case null:
                        return false;
                    case bool bVal:
                        boolValue = bVal;
                        break;
                }

                if (value is List<ThumbnailOverlayToggleButtonRenderer> ltbRenderer) count = ltbRenderer.Count;
                else
                    overlayBool = true;
            }

            if (overlayBool)
                return overlayBool && boolValue;
            return (count > 0 && boolValue);
        }
    }
}