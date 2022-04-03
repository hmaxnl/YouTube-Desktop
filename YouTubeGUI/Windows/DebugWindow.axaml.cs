using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Serilog;

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
            /*TextEditor debugTextEditor = this.Find<TextEditor>("DebugTextEditor");
            if (debugTextEditor == null) return;
            if (Program.DmInstance != null) Program.DmInstance.LogTerminal.TextEditor = debugTextEditor;*/
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        public void DebugCommandTest()
        {
            Log.Information("Btn clicked!");
        }
    }
}