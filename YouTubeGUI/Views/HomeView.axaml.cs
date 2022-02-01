using Avalonia.Controls;
using Avalonia.Markup.Xaml;
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

        private void RecyclingElementFactory_OnSelectTemplateKey(object? sender, SelectTemplateEventArgs e)
        {
            switch (e.DataContext)
            {
                case RichItemRenderer rir:
                    switch (rir.Content)
                    {
                        case RichVideoContent:
                            e.TemplateKey = "VideoItem";
                            break;
                        default:
                            break;
                    }
                    break;
                /*case RichSectionRenderer rsr:
                    switch (rsr.Content)
                    {
                        case RichShelfRenderer:
                            //e.TemplateKey = "Section";
                            break;
                    }
                    break;*/
                default:
                    e.TemplateKey = "DefItem";
                    break;
            }
        }
    }
}