using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ninject;

namespace MinecraftForwardManagingModule
{
    public class BackgroundWorker
    {
        public bool IsRunning { get; set; }

        private readonly ModuleController _moduleController;
        private readonly IKernel _kernel;

        private MinecraftForwardRefresher _minecraftForwardRefresher;
        private Thread _t;
        public BackgroundWorker(ModuleController moduleController, IKernel kernel)
        {
            IsRunning = false;
            _moduleController = moduleController;
            _kernel = kernel;
        }

        public void Start()
        {
            _minecraftForwardRefresher = new MinecraftForwardRefresher(_moduleController, _kernel);
            IsRunning = true;
            _t = new Thread(RepeatThreadWork) { IsBackground = true };
            _t.SetApartmentState(ApartmentState.STA);
            _t.Start();
            _moduleController.TempLog.Add("Module started...");
        }

        public void Stop()
        {
            IsRunning = false;
            while (_t.ThreadState != ThreadState.Stopped)
            {
                Thread.Sleep(10);
            }
            _minecraftForwardRefresher.TryStopForward();
            _moduleController.TempLog.Add("Module stopped...");
        }

        private int _interval = 240_000;
        private int _counter = 300_000;
        private void RepeatThreadWork()
        {
            while (IsRunning)
            {
                if (_counter >= _interval)
                {
                    _counter = 0;
                    _minecraftForwardRefresher.RefreshCycle();
                }

                Thread.Sleep(10_000);
                _counter += 10_000;
            }
        }
    }
}