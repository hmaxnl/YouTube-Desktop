using Avalonia.Controls;

namespace YouTubeGUI.Controls.Content
{
    public class SectionRenderer : UserControl
    {
        public SectionRenderer()
        { }

        public SectionRenderer(string classes)
        {
            Classes = new Classes(classes);
        }
    }
}