using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CustomModule.Contract;
using ForwardingControllerGUI.Core;
using Ninject;
using SharedItems.Model;

namespace ForwardingControllerGUI.ViewModel
{
    public class CustomModulesUserControlViewModel : ObservableObject
    {
        public ICustomModuleController CurrentModule { get; set; }
        public Visibility ContentVisibility { get; set; }

        public RelayCommand StartCommand { get; set; }
        public RelayCommand StopCommand { get; set; }

        public bool IsStartButtonEnabled { get; set; }
        public bool IsStopButtonEnabled { get; set; }

        private IKernel _kernel;
        public CustomModulesUserControlViewModel(ICustomModuleController module, IKernel kernel)
        {
            _kernel = kernel;

            ContentVisibility = Visibility.Collapsed;

            if (module == null)
                return;

            CurrentModule = module;
            ContentVisibility = Visibility.Visible;

            IsStartButtonEnabled = !CurrentModule.IsRunning;
            IsStopButtonEnabled = !IsStartButtonEnabled;

            StartCommand = new RelayCommand(StartModule);
            StopCommand = new RelayCommand(StopModule);
        }

        private void StartModule(object parameter)
        {
            Task.Run(() =>
            {
                CurrentModule.TempLog = new List<string>(new[] { "Starting..." });
                CurrentModule.Run(_kernel);
                CurrentModule.TempLog = new List<string>(new[] { "Started..." });

                IsStartButtonEnabled = !CurrentModule.IsRunning;
                IsStopButtonEnabled = !IsStartButtonEnabled;
            });
        }

        private void StopModule(object parameter)
        {
            Task.Run(() =>
            {
                CurrentModule.TempLog = new List<string>(new[] { "Stopping..." });
                CurrentModule.Stop();
                CurrentModule.TempLog = new List<string>(new[] { "Stopped..." });

                IsStartButtonEnabled = !CurrentModule.IsRunning;
                IsStopButtonEnabled = !IsStartButtonEnabled;
            });
        }
    }
}