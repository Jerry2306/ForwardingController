using CustomModuleUtils.Contract;
using Ninject;
using SharedItems.Model;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ForwardingControllerGUI.ViewModel
{
    public class ExitUserControlViewModel : ObservableObject
    {
        public string StateLine1 { get; set; }
        public string StateLine2 { get; set; }

        private Dispatcher _dispatcher;
        private IKernel _kernel;
        public ExitUserControlViewModel(Dispatcher dispatcher, IKernel kernel)
        {
            _dispatcher = dispatcher;
            _kernel = kernel;

            Task.Run(StartExitApplication);
        }

        private void StartExitApplication()
        {
            StateLine1 = "Stopping custom modules...";
            StopCustomModules();

            _dispatcher.Invoke(() => Application.Current.Shutdown(0));
        }

        private void StopCustomModules()
        {
            var customModuleManager = _kernel.Get<ICustomModuleManager>();
            var modules = customModuleManager.CustomModules.Select(x => x.Value).ToList();

            for (int i = 0; i < modules.Count; i++)
            {
                StateLine2 = $"{modules[i].Name} ({i + 1}/{modules.Count})";
                modules[i].Stop();
                Thread.Sleep(1000);
            }
        }
    }
}