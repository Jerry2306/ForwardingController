using ForwardingControllerGUI.ViewModel;
using Ninject;
using System.Windows.Controls;

namespace ForwardingControllerGUI.View.UserControls
{
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