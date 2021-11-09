using CustomModule.Contract;
using Ninject;
using System.Collections.Generic;
using System.Threading;

namespace DefaultModule
{
    public class ModuleController : ICustomModuleController
    {
        public string IconKind => "CloudQuestion";
        public string Name => "Default Module";
        public IDictionary<string, string> Configuration { get; set; }

        public List<string> TempLog { get; set; } = new List<string>();

        private IKernel _kernel;
        private Thread _t;

        private bool _isRunning = false;
        public bool IsRunning => _isRunning;

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
            var i = 0;
            while (_isRunning)
            {
                i++;
                Thread.Sleep(5000);
                TempLog.Add("Test log - " + i);
                //MessageBox.Show(Configuration["TestConfiguration"]);
            }
        }
    }
}