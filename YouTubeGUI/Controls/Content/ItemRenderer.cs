using Avalonia.Controls;

namespace YouTubeGUI.Controls.Content
{
    public class ItemRenderer : UserControl
    {
        public ItemRenderer()
        { }
        public ItemRenderer(string[] classes)
        {
            Classes = new Classes(classes);
        }

        public ItemRenderer(string classes)
        {
            Classes = new Classes(classes);
        }
    }
}