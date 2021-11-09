using ForwardingControllerGUI.View;
using ForwardingControllerGUI.ViewModel;
using Ninject;
using NinjectBindingManaging;
using SharedItems.Extensions;
using System;
using System.Reflection;
using System.Text;
using System.Windows;

namespace ForwardingControllerGUI
{
    public partial class App : Application
    {
        private MainWindowViewModel _mainVm;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var kernel = GetKernel();

            _mainVm = LoadMainWindowAndViewModel(kernel);
        }

        private IKernel GetKernel()
        {
            try
            {
                return new NinjectBindingManager().GetKernel();
            }
            catch (Exception exc)
            {
                DisplayException("Fatal error at loading kernel:", exc);
                Current.Shutdown(-1);
            }

            return null;
        }

        private MainWindowViewModel LoadMainWindowAndViewModel(IKernel kernel)
        {
            MainWindowViewModel vm = null;

            try
            {
                vm = kernel.Get<MainWindowViewModel>();
                var window = new MainWindow
                {
                    DataContext = vm,
                    VM = vm
                };

                window.Show();
            }
            catch (ReflectionTypeLoadException exc)
            {
                DisplayException("Fatal error at reflectionloading main window -> commonly at loading custom modules:", exc);
                Current.Shutdown(-1);
            }
            catch (Exception exc)
            {
                DisplayException("Fatal error at loading main window:", exc);
                Current.Shutdown(-1);
            }

            return vm;
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            DisplayException("An unhandled exception occured. The programm may be able to stay running:", e.Exception);
        }

        private void DisplayException(string mainMessage, Exception exc)
        {
            string excString;

            if (exc != null)
            {
                var excBuilder = new StringBuilder();
                excBuilder.AppendLine(mainMessage);
                excBuilder.AppendLine(exc.GetFormattedMessage());
                excBuilder.AppendLine($"Stacktrace: {exc.StackTrace}");
                excString = excBuilder.ToString();
            }
            else
                excString = mainMessage;

            MessageBox.Show(excString);
        }
    }
}