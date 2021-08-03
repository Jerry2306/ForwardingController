using ForwardingControllerGUI.Core;
using ForwardingControllerGUI.View.UserControls;
using Ninject;
using SharedItems.Model;
using System.Windows;
using ForwardingControllerGUI.Model;

namespace ForwardingControllerGUI.ViewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        public object CurrentView { get; set; }

        public bool IsNavigationEnabled { get; set; }
        public bool IsCloseButtnChecked { get; set; }

        public RelayCommand OverviewCommand { get; set; }
        public RelayCommand LoggingCommand { get; set; }
        public RelayCommand ModulesCommand { get; set; }
        public RelayCommand SettingsCommand { get; set; }
        public RelayCommand ExitCommand { get; set; }

        public MainWindowModulesSubViewModel ModulesSubVm { get; set; }

        private readonly IKernel _kernel;

        public MainWindowViewModel() { }

        public MainWindowViewModel(IKernel kernel)
        {
            _kernel = kernel;

            IsNavigationEnabled = true;
            IsCloseButtnChecked = false;

            OverviewCommand = new RelayCommand(o => SetCurrentView(_kernel.Get<OverviewUserControl>()));
            LoggingCommand = new RelayCommand(o => SetCurrentView(null));
            ModulesCommand = new RelayCommand(o => SetCurrentView(_kernel.Get<CustomModulesUserControl>()));
            SettingsCommand = new RelayCommand(o => SetCurrentView(null));
            ExitCommand = new RelayCommand(ExitApplication);

            ModulesSubVm = _kernel.Get<MainWindowModulesSubViewModel>();

            SetCurrentView(_kernel.Get<OverviewUserControl>());
        }

        public void ExitApplication(object parameter)
        {
            if (MessageBox.Show("Sicher, dass du die Anwendung beenden willst?", "Achtung", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                IsCloseButtnChecked = false;
                return;
            }

            IsNavigationEnabled = false;
            SetCurrentView(_kernel.Get<ExitUserControl>());
        }

        private void SetCurrentView(object newView)
        {
            if (CurrentView?.GetType() == newView?.GetType())
                return;

            if (newView?.GetType() == typeof(CustomModulesUserControl))
            {
                ModulesSubVm.ModuleListVisibility = Visibility.Visible;
                ModulesSubVm.ShowProgressBar();
            }
            else
            {
                ModulesSubVm.ModuleListVisibility = Visibility.Hidden;
                ModulesSubVm.HideProgressBar();
            }

            CurrentView = newView;
        }
    }
}