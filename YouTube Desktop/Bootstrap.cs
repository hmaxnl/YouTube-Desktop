using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YouTube_Desktop.Views;

namespace YouTube_Desktop
{
    public class Bootstrap
    {
        static Window _mainWindow;
        public Bootstrap()
        {
            _mainWindow = new MainWindow
            {
                Title = "YTD <Example application>"
            } as Window;
            _mainWindow.Show();
        }
    }
}
