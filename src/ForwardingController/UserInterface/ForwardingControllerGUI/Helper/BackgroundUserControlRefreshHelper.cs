using System;
using System.Threading;
using System.Threading.Tasks;

namespace ForwardingControllerGUI.Helper
{
    public sealed class BackgroundUserControlRefreshHelper
    {
        private static object _instanceLock = new object();
        private static BackgroundUserControlRefreshHelper _instance;

        public static void Initialize()
        {
            lock (_instanceLock)
            {
                if (_instance != null)
                    throw new Exception("Instance already initialized!");

                _instance = new BackgroundUserControlRefreshHelper();
            }
        }

        public static void AttachAction(int interval, Action action)
        {
            lock (_instanceLock)
            {
                if (interval <= 0) throw new ArgumentOutOfRangeException(nameof(interval), "interval must be greater than 0");
                _instance.Attach(interval, action);
            }
        }

        private BackgroundUserControlRefreshHelper() { }

        private bool _isRunning = false;
        private bool _cancel = false;
        private int _counter = 0;
        private void Attach(int interval, Action action)
        {
            if (_isRunning)
            {
                _cancel = true;
                while (_isRunning) { Thread.Sleep(new Random().Next(5, 45)); }
            }

            StartCycle(interval, action);
        }

        private void StartCycle(int interval, Action action)
        {
            Task.Run(() =>
            {
                _counter = 0;
                while (!_cancel)
                {
                    _isRunning = true;

                    if (_counter >= Math.Ceiling(interval / 10d))
                    {
                        _counter = 0;

                        action.Invoke();
                    }

                    Thread.Sleep(10);
                    _counter++;
                }

                _isRunning = false;
                _cancel = false;
            });
        }
    }
}