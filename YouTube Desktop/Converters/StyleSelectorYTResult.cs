using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace YouTube_Desktop.Converters
{
    public class StyleSelectorYTResult : StyleSelector
    {
        public Style Style_Video { get; set; }
        public Style Style_Channel { get; set; }
        public Style Style_PlayList { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            Style styleToUse = null;
            SearchResult searchResultItem = item as SearchResult;
            if (searchResultItem.Id.Kind == "youtube#video")
                styleToUse = Style_Video;
            else if (searchResultItem.Id.Kind == "youtube#playlist")
                styleToUse = Style_PlayList;
            else if (searchResultItem.Id.Kind == "youtube#channel")
                styleToUse = Style_Channel;
            else
                throw new NotSupportedException("No valid kind of result received!");

            if (styleToUse == null)
                throw new NotSupportedException("Required style was empty!");
            return styleToUse;
        }
    }
}