using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using YouTubeScrap.Data.Extend;

namespace YouTubeGUI.Controls
{
    public class HomeScreenContentList : UserControl
    {
        public HomeScreenContentList()
        {
            InitializeComponent();
            ContentListProperty.Changed.Where(args => args.IsEffectiveValueChange)
                .Subscribe(args => FilterItems((HomeScreenContentList)args.Sender, args.NewValue.Value));
        }

        public static readonly AttachedProperty<List<ContentRender>> ContentListProperty =
            AvaloniaProperty.RegisterAttached<HomeScreenContentList, List<ContentRender>>("ContentList", typeof(HomeScreenContentList));

        public static List<ContentRender> GetContentList(HomeScreenContentList element)
        {
            return element.GetValue(ContentListProperty);
        }

        public static void SetContentList(HomeScreenContentList element, List<ContentRender> value)
        {
            element.SetValue(ContentListProperty, value);
        }

        private readonly List<ContentRender> _itemRenderers = new List<ContentRender>();
        private readonly List<ContentRender> _sectionRenderers = new List<ContentRender>();
        private ContentRender _continuationItem;

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        private void FilterItems(HomeScreenContentList sender, List<ContentRender> list)
        {
            if (list.Count <= 0) return;
            _itemRenderers.Clear();
            _sectionRenderers.Clear();
            
            foreach (var content in list)
            {
                if (content.RichItem != null)
                {
                    _itemRenderers.Add(content);
                    continue;
                }
                if (content.RichSection != null)
                {
                    _sectionRenderers.Add(content);
                    continue;
                }
                if (content.ContinuationItem != null)
                {
                    _continuationItem = content;
                    continue;
                }
            }
        }
    }
}