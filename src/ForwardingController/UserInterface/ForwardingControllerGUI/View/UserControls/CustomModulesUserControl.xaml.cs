using CustomModule.Contract;
using ForwardingControllerGUI.ViewModel;
using System.Windows.Controls;
using Ninject;

namespace ForwardingControllerGUI.View.UserControls
{
    public partial class CustomModulesUserControl : UserControl
    {
        public CustomModulesUserControlViewModel ViewModel { get; set; }
        public CustomModulesUserControl(ICustomModuleController module, IKernel kernel)
        {
            InitializeComponent();

            ViewModel = new CustomModulesUserControlViewModel(module, kernel);
            DataContext = ViewModel;
        }
    }
}