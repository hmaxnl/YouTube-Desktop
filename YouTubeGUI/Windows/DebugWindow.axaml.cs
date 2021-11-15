using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaEdit;

namespace YouTubeGUI.Windows
{
    public class DebugWindow : Window
    {
        public DebugWindow()
        {
            InitializeComponent();
            DataContext = this;
#if DEBUG
            this.AttachDevTools();
#endif
            TextEditor debugTextEditor = this.Find<TextEditor>("DebugTextEditor");
            Terminal.Terminal.TextEditor = debugTextEditor;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        public void DebugCommandTest()
        {
            Terminal.Terminal.AppendLog("Command executed!");
        }
    }
}