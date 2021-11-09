using CustomModule.Contract;
using CustomModuleUtils.Contract;
using ForwardingControllerGUI.Model;
using Ninject;
using SharedItems.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ForwardingControllerGUI.ViewModel
{
    public class MainWindowModulesSubViewModel : ObservableObject
    {
        public IEnumerable<ICustomModuleController> AvailableModules { get; set; }

        public ProgressBarModel ProgressBar { get; set; }

        public Visibility ModuleListVisibility { get; set; }

        private readonly ProgressBarModel _tempProgressBar;
        private readonly ICustomModuleManager _customModuleManager;

        public MainWindowModulesSubViewModel() { }
        public MainWindowModulesSubViewModel(ICustomModuleManager customModuleManager, IKernel kernel)
        {
            _customModuleManager = customModuleManager;
            ModuleListVisibility = Visibility.Hidden;

            ProgressBar = new ProgressBarModel();

            _tempProgressBar = new ProgressBarModel
            {
                Visibility = Visibility.Visible,
                Header = "Module wurden noch nicht vollständig geladen. Bitte warten!"
            };

            LoadCustomModules(kernel);
        }

        public void ShowProgressBar() => ProgressBar = _tempProgressBar;
        public void HideProgressBar() => ProgressBar = new ProgressBarModel();

        private void LoadCustomModules(IKernel kernel)
        {
            Task.Run(() =>
            {
                try
                {
                    Thread.Sleep(1000);
                    _customModuleManager.RunCustomModules(kernel);
                    CustomModulesLoadedSuccessful();
                }
                catch (Exception exc)
                {
                    CustomModulesLoadedError(exc);
                }
            });
        }

        private void CustomModulesLoadedSuccessful()
        {
            AvailableModules = _customModuleManager.CustomModules.Select(x => x.Value);
            _tempProgressBar.Clear();
        }

        private void CustomModulesLoadedError(Exception exc)
        {
            //DisplayException("Error at loading custom modules:", exc); TODO: log exception

            _tempProgressBar.Clear();
            _tempProgressBar.Visibility = Visibility.Visible;
            _tempProgressBar.Header = "Während dem Laden der Module ist ein Fehler aufgetreten!";
            _tempProgressBar.Description1 = "Der reibungslose Ablauf der Module kann nicht gewährleistet werden! Die Module werden nicht geladen!";
            _tempProgressBar.Description2 = exc.Message;
            _tempProgressBar.IsIntermediate = false;
            _tempProgressBar.Value = 100;
            _tempProgressBar.Foreground = ProgressBarModel.ErrorColor;
        }
    }
}