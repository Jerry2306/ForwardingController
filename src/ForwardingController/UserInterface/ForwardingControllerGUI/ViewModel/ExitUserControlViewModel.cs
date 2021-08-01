using Ninject;
using SharedItems.Model;
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
        public ExitUserControlViewModel(Dispatcher dispatcher, IKernel kernel)
        {
            _dispatcher = dispatcher;

            StateLine1 = "Custom modules";
            StateLine2 = "DefaultModule";

            //Task.Run(StartExitApplication);
        }

        private void StartExitApplication()
        {
            Thread.Sleep(4000);

            StateLine1 = "TunnelingForward";
            StateLine2 = "Tunnel 123 wird geschlossen";

            Thread.Sleep(7000);

            _dispatcher.Invoke(() => Application.Current.Shutdown(0));
        }
    }
}