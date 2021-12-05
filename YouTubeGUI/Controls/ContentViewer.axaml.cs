using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using YouTubeScrap.Data.Extend;

namespace YouTubeGUI.Controls
{
    //TODO: Found out the text aligner and/or wrapping cannot handle left aligned text!
    public class ContentViewer : UserControl
    {
        public ContentViewer()
        {
            DataContext = this;
            InitializeComponent();
            ContentListProperty.Changed.Where(args => args.IsEffectiveValueChange)
                .Subscribe(args => FilterItems((ContentViewer)args.Sender, args.NewValue.Value));
        }

        public static readonly AttachedProperty<List<ContentRender>> ContentListProperty =
            AvaloniaProperty.RegisterAttached<ContentViewer, List<ContentRender>>("ContentList", typeof(ContentViewer));
        public static List<ContentRender> GetContentList(ContentViewer element)
        {
            return element.GetValue(ContentListProperty);
        }
        public static void SetContentList(ContentViewer element, List<ContentRender> value)
        {
            element.SetValue(ContentListProperty, value);
        }

        public List<ContentRender> ItemRenderers { get; } = new List<ContentRender>();
        public List<ContentRender> SectionRenderers { get; } = new List<ContentRender>();
        public List<object> CompleteList { get; } = new List<object>();
        private ContentRender _continuationItem;

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        private void FilterItems(ContentViewer sender, List<ContentRender> list)
        {
            if (list.Count <= 0) return;
            ItemRenderers.Clear();
            SectionRenderers.Clear();
            foreach (var content in list)
            {
                if (content.RichItem != null)
                {
                    ItemRenderers.Add(content);
                    continue;
                }
                if (content.RichSection != null)
                {
                    SectionRenderers.Add(content);
                    continue;
                }
                if (content.ContinuationItem != null)
                {
                    _continuationItem = content;
                    continue;
                }
            }
            CompleteList.Add(new ItemContents() { Items = ItemRenderers });
            CompleteList.AddRange(SectionRenderers);
        }
    }
    public class ItemContents
    {
        public List<ContentRender> Items { get; set; }
    }
}