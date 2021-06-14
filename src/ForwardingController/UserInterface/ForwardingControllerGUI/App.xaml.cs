using ForwardingControllerGUI.View;
using ForwardingControllerGUI.ViewModel;
using Ninject;
using NinjectBindingManaging;
using SharedItems.Extensions;
using System;
using System.Text;
using System.Windows;

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

            try
            {
                var kernel = new NinjectBindingManager().GetKernel();

                var window = new MainWindow
                {
                    DataContext = kernel.Get<MainWindowViewModel>()
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
            }
        }
    }
}