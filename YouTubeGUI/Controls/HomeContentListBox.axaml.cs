using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using YouTubeScrap.Data.Extend;

namespace YouTubeGUI.Controls
{
    public class HomeContentListBox : UserControl
    {
        public HomeContentListBox()
        {
            //DataContext = this;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public static readonly StyledProperty<List<ContentRender>> ContentItemListProperty =
            AvaloniaProperty.Register<HomeContentListBox, List<ContentRender>>(nameof(ContentItemList));

        public List<ContentRender> ContentItemList
        {
            get => GetValue(ContentItemListProperty);
            set => SetValue(ContentItemListProperty, value);
        }
    }
}