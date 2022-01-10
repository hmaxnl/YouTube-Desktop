using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using YouTubeGUI.Core.XamlTools;
using YouTubeGUI.Models.Snippets;
using YouTubeScrap.Data.Renderers;

namespace YouTubeGUI.Views
{
    public class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        private void ElementFactory_OnSelectTemplateKey(object? sender, SelectTemplateEventArgs e)
        {
            switch (e.DataContext)
            {
                case ItemSection:
                    e.TemplateKey = "itemSectionTemp";
                    break;
                case RichSectionRenderer:
                    e.TemplateKey = "sectionTemp";
                    break;
            }
        }
    }
}