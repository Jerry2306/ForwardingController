using System;
using System.Collections.Generic;
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
using ForwardingControllerGUI.ViewModel;
using Ninject;

namespace ForwardingControllerGUI.View.UserControls
{
    /// <summary>
    /// Interaction logic for ExitUserControl.xaml
    /// </summary>
    public partial class ExitUserControl : UserControl
    {
        public ExitUserControlViewModel VM { get; set; }
        public ExitUserControl(IKernel kernel)
        {
            InitializeComponent();

            VM = new ExitUserControlViewModel(Dispatcher, kernel);
            DataContext = VM;
        }
    }
}