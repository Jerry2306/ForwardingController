using CustomModule.Contract;
using Ninject;
using System.Collections.Generic;

namespace MinecraftForwardManagingModule
{
    public class ModuleController : ICustomModuleController
    {
        public string Name => "Minecraft Managing";
        public string IconKind => "Minecraft";

        public IDictionary<string, string> Configuration { get; set; }
        
        public bool IsRunning => _backgroundWorker?.IsRunning ?? false;

        public List<string> TempLog { get; set; } = new List<string>();

        private BackgroundWorker _backgroundWorker;
        public void Run(IKernel kernel)
        {
            _backgroundWorker = new BackgroundWorker(this, kernel);
            _backgroundWorker.Start();
        }

        public void Stop()
        {
            _backgroundWorker.Stop();
        }
    }
}