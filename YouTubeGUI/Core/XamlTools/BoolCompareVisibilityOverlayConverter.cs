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
            if (values.Count < 2)
                return false;
            int count = 0;
            bool boolValue = false;
            bool overlayBool = false;
            foreach (object value in values)
            {
                if (value == null) return false;
                switch (value)
                {
                    case ContentRender cRender:
                        //overlayBool = (cRender.Variables.Overlay.TimeStatusOverlay != null);
                        overlayBool = (cRender.Variables.Overlay.TimeStatusOverlay != null) || (cRender.Variables.Overlay.NowPlayingOverlay != null) || (cRender.Variables.Overlay.HoverTextOverlay != null)
                                      || (cRender.Variables.Overlay.BottomPanelOverlay != null) || (cRender.Variables.Overlay.EndorsementOverlay != null) || (cRender.Variables.Overlay.ResumePlaybackOverlay != null);
                        count = cRender.Variables.Overlay.ToggleButtonOverlays.Count;
                        break;
                    /*case ThumbnailOverlayView oView:
                        overlayBool = (oView.TimeStatusOverlay != null) || (oView.NowPlayingOverlay != null) || (oView.HoverTextOverlay != null)
                                      || (oView.BottomPanelOverlay != null) || (oView.EndorsementOverlay != null) || (oView.ResumePlaybackOverlay != null);
                        count = oView.ToggleButtonOverlays.Count;
                        break;*/
                    case bool boolVal:
                        boolValue = boolVal;
                        break;
                    default:
                        return false;
                }
            }

            if (overlayBool)
                return overlayBool && boolValue;
            return (count > 0 && boolValue);
        }
    }
}