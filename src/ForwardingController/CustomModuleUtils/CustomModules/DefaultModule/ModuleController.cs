using CustomModule.Contract;
using Ninject;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

namespace DefaultModule
{
    class ModuleController : ICustomModuleController
    {
        public IDictionary<string, string> Configuration { get; set; }

        private IKernel _kernel;
        private Thread _t;
        private bool isRunning = false;
        public void Run(IKernel kernel)
        {
            _kernel = kernel;

            _t = new Thread(ThreadWork);
            _t.IsBackground = true;
            _t.SetApartmentState(ApartmentState.STA);
            _t.Start();
        }

        public void Stop()
        {
            isRunning = false;
            while (_t.ThreadState != ThreadState.Stopped)
            {
                Thread.Sleep(10);
            }
        }

        private void ThreadWork()
        {
            isRunning = true;

            while (isRunning)
            {
                Thread.Sleep(5000);
                MessageBox.Show(Configuration["TestConfiguration"]);
            }
        }
    }
}