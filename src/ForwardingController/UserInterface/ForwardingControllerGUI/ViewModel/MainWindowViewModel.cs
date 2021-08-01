using System;
using System.Threading;
using ForwardingControllerGUI.Core;
using ForwardingControllerGUI.View.UserControls;
using Ninject;
using SharedItems.Model;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace ForwardingControllerGUI.ViewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        public object CurrentView { get; set; }

        public bool IsNavigationEnabled { get; set; }
        public bool IsCloseButtnChecked { get; set; }

        public RelayCommand ExitCommand { get; set; }
        public RelayCommand OverviewCommand { get; set; }

        private readonly IKernel _kernel;

        public MainWindowViewModel() { }

        public MainWindowViewModel(IKernel kernel)
        {
            _kernel = kernel;

            SetCurrentView(_kernel.Get<OverviewUserControl>());

            IsNavigationEnabled = true;
            IsCloseButtnChecked = false;

            ExitCommand = new RelayCommand(ExitApplication);
            OverviewCommand = new RelayCommand(o => SetCurrentView(_kernel.Get<OverviewUserControl>()));
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
            if (CurrentView == null)
            {
                CurrentView = newView;
                return;
            }

            if (CurrentView.GetType() != newView.GetType())
                CurrentView = newView;
        }
    }
}