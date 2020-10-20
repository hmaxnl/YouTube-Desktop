using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YouTube_Desktop.Core;

namespace YouTube_Desktop.Control
{
    /// <summary>
    /// Interaction logic for VideoListControl.xaml
    /// </summary>
    public partial class VideoListControl : UserControl, INotifyPropertyChanged
    {
        // Ctor
        public VideoListControl()
        {
            InitializeComponent();
        }

        // Publics
        public static readonly DependencyProperty StructDataPtrProperty = DependencyProperty.Register("StructDataPtr", typeof(IntPtr), typeof(VideoListControl), new PropertyMetadata(IntPtr.Zero));

        public event PropertyChangedEventHandler PropertyChanged;

        public IntPtr StructDataPtr
        {
            get => (IntPtr)GetValue(StructDataPtrProperty);
            set
            {
                SetValue(StructDataPtrProperty, value);
                OnPropertyChanged(nameof(StructDataPtr));
            }
        }

        // Privates
        private void OnPropertyChanged(string PropName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(PropName));
        }
    }
}