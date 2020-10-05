using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using YouTube_Desktop.Core;
using YouTube_Desktop.Views;

namespace YouTube_Desktop.ViewModel
{
    public class MainWindowModel
    {
        public MainWindowModel()
        {
        }
        public ICommand ToggleMenuCommand
        {
            get => new CommandHandler(() => ToggleMenu(), () => CanExecute);
        }
        public ICommand HomeButtonCommand
        {
            get => new CommandHandler(() => ToggleMenu(), () => CanExecute);
        }
        public bool CanExecute
        {
            get => Application.Current.Dispatcher.CheckAccess();
        }
        public void ToggleMenu()
        {
            //ResourceDictionary rdict = new ResourceDictionary();
            //rdict.Source = new Uri("pack://application:,,,/Styles/DefaultStyle.xaml", UriKind.Absolute);
            //Application.Current.Resources["AppBackgroundColor"] = Color.FromRgb(0, 0, 100);
            //Application.Current.Resources.MergedDictionaries.Clear();
            //Application.Current.Resources.MergedDictionaries.Add(rdict);
            FrameworkElement _windowElement = Application.Current.MainWindow.Content as FrameworkElement;
            if (_windowElement.FindName("AppMenu") is UserControl _menuControl)
            {
                if (_menuControl.Dispatcher.CheckAccess())// Check acces if called from other thread!
                {
                    if (_menuControl.Visibility == Visibility.Visible)
                        _menuControl.Visibility = Visibility.Collapsed;
                    else
                        _menuControl.Visibility = Visibility.Visible;
                }
                else
                {
                    //TODO: Needs a delagate and make a invoke!
                }
            }
        }
    }
}