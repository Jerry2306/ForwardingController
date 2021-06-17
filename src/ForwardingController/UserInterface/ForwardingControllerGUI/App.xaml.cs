using ForwardingControllerGUI.View;
using ForwardingControllerGUI.ViewModel;
using Ninject;
using NinjectBindingManaging;
using SharedItems.Extensions;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CustomModuleUtils.Contract;

namespace ForwardingControllerGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IKernel kernel;
            MainWindowViewModel vm;
            try
            {
                kernel = new NinjectBindingManager().GetKernel();
                vm = kernel.Get<MainWindowViewModel>();

                var window = new MainWindow
                {
                    DataContext = vm,
                    VM = vm
                };

                window.Show();
            }
            catch (Exception exc)
            {
                var excBuilder = new StringBuilder();
                excBuilder.AppendLine("Fatal error at loading main window:");
                excBuilder.AppendLine(exc.GetFormattedMessage());
                excBuilder.AppendLine($"Stacktrace: {exc.StackTrace}");
                MessageBox.Show(excBuilder.ToString());

                Current.Shutdown(-1);
                return;
            }

            Task.Run(() =>
            {
                try
                {
                    kernel.Get<ICustomModuleManager>().RunCustomModules(kernel);
                }
                catch (Exception exc)
                {
                    var excBuilder = new StringBuilder();
                    excBuilder.AppendLine("Error at loading custom modules:");
                    excBuilder.AppendLine(exc.GetFormattedMessage());
                    excBuilder.AppendLine($"Stacktrace: {exc.StackTrace}");
                    MessageBox.Show(excBuilder.ToString());
                }
            });
        }
    }
}