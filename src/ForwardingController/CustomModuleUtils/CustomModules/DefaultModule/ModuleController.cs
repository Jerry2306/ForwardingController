using CustomModule.Contract;
using Ninject;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

namespace DefaultModule
{
    public class ModuleController : ICustomModuleController
    {
        public string Name { get; set; }
        public IDictionary<string, string> Configuration { get; set; }

        private IKernel _kernel;
        private Thread _t;
        private bool _isRunning = false;
        public ModuleController()
        {
            Name = "Default";
        }

        public void Run(IKernel kernel)
        {
            _kernel = kernel;

            _t = new Thread(ThreadWork) { IsBackground = true };
            _t.SetApartmentState(ApartmentState.STA);
            _t.Start();
        }

        public void Stop()
        {
            _isRunning = false;
            while (_t.ThreadState != ThreadState.Stopped)
            {
                Thread.Sleep(10);
            }
        }

        private void ThreadWork()
        {
            _isRunning = true;

            while (_isRunning)
            {
                Thread.Sleep(5000);
                MessageBox.Show(Configuration["TestConfiguration"]);
            }
        }
    }
}