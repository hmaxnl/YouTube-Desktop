using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using YouTubeScrap.Data.Extend;

namespace YouTubeGUI.Controls
{
    //BUG: Found out the text aligner and/or wrapping cannot handle right aligned text!
    public class ContentViewer : UserControl
    {
        public ContentViewer()
        {
            //DataContext = this;
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

        public List<object> ItemRenderers { get; } = new List<object>();
        public List<object> SectionRenderers { get; } = new List<object>();
        public List<object> CompleteList { get; } = new List<object>();
        private ContentRender _continuationItem;

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        private void FilterItems(ContentViewer sender, List<ContentRender> list)
        {
            /*if (list is not { Count: > 0 }) return;
            ItemRenderers.Clear();
            SectionRenderers.Clear();
            CompleteList.Clear();
            foreach (var content in list)
            {
                if (content.RichItem != null)
                {
                    if (content.RichItem.Content.VideoRenderer != null)
                        ItemRenderers.Add(content.RichItem.Content.VideoRenderer);
                    if (content.RichItem.Content.RadioRenderer != null)
                        ItemRenderers.Add(content.RichItem.Content.RadioRenderer);
                    if (content.RichItem.Content.DisplayAdRenderer != null)
                        ItemRenderers.Add(content.RichItem.Content.DisplayAdRenderer);
                    //ItemRenderers.Add(content);
                    continue;
                }
                if (content.RichSection != null)
                {
                    if (content.RichSection.RichSectionContent.RichShelfRenderer != null)
                        SectionRenderers.Add(content.RichSection.RichSectionContent.RichShelfRenderer);
                    if (content.RichSection.RichSectionContent.InlineSurveyRenderer != null)
                        SectionRenderers.Add(content.RichSection.RichSectionContent.InlineSurveyRenderer);
                    if (content.RichSection.RichSectionContent.PromotedItemRenderer != null)
                        SectionRenderers.Add(content.RichSection.RichSectionContent.PromotedItemRenderer);
                    //SectionRenderers.Add(content);
                    continue;
                }
                if (content.ContinuationItem != null)
                {
                    _continuationItem = content;
                    continue;
                }
            }
            CompleteList.Add(new ItemContents() { Items = ItemRenderers });
            CompleteList.AddRange(SectionRenderers);*/
        }
    }
    public class ItemContents
    {
        public List<object> Items { get; set; }
    }
}