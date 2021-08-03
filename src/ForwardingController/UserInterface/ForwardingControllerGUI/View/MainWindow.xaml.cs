using ForwardingControllerGUI.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace ForwardingControllerGUI.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel VM { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SpTopMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
                WindowState = (WindowState)(WindowState == 0 ? 2 : 0);

            DragMove();
        }

        private void Window_Closing(object sender, CancelEventArgs e) => e.Cancel = true;
    }
}