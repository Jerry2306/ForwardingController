using CustomModule.Contract;
using ForwardingControllerGUI.Core;
using ForwardingControllerGUI.Helper;
using ForwardingControllerGUI.Model;
using Ninject;
using SharedItems.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ForwardingControllerGUI.ViewModel
{
    public class CustomModulesUserControlViewModel : ObservableObject
    {
        public ICustomModuleController CurrentModule { get; set; }
        public Visibility ContentVisibility { get; set; }

        public RelayCommand StartCommand { get; set; }
        public RelayCommand StopCommand { get; set; }
        public RelayCommand ClearLogCommand { get; set; }

        public CustomModulesSubModel CustomModulesSubModel { get; set; }

        private IKernel _kernel;
        public CustomModulesUserControlViewModel(ICustomModuleController module, IKernel kernel)
        {
            _kernel = kernel;

            ContentVisibility = Visibility.Collapsed;

            if (module == null)
                return;

            CurrentModule = module;
            ContentVisibility = Visibility.Visible;

            CustomModulesSubModel = new CustomModulesSubModel(module);

            BackgroundUserControlRefreshHelper.AttachAction(333, RefreshWork);

            StartCommand = new RelayCommand(StartModule);
            StopCommand = new RelayCommand(StopModule);
            ClearLogCommand = new RelayCommand(ClearLog);
        }

        private void StartModule(object parameter)
        {
            Task.Run(() =>
            {
                CurrentModule.TempLog.Add("Starting...");
                CurrentModule.Run(_kernel);
                CurrentModule.TempLog.Add("Started...");
            });
        }

        private void StopModule(object parameter)
        {
            Task.Run(() =>
            {
                CurrentModule.TempLog.Add("Stopping...");
                CurrentModule.Stop();
                CurrentModule.TempLog.Add("Stopped...");
            });
        }

        private void ClearLog(object parameter) => CurrentModule.TempLog.Clear();

        private void RefreshWork()
        {
            if (CurrentModule.IsRunning)
            {
                CustomModulesSubModel.ActiveColor = Brushes.Green;
                CustomModulesSubModel.ActiveText = "Active";
                CustomModulesSubModel.IsStartButtonEnabled = false;
                CustomModulesSubModel.IsStopButtonEnabled = true;
            }
            else
            {
                CustomModulesSubModel.ActiveColor = Brushes.Red;
                CustomModulesSubModel.ActiveText = "Inactive";
                CustomModulesSubModel.IsStartButtonEnabled = true;
                CustomModulesSubModel.IsStopButtonEnabled = false;
            }

            var list = new List<string>();
            CurrentModule.TempLog.ForEach(x => list.Add(x));

            CustomModulesSubModel.Logs = list;
        }
    }
}