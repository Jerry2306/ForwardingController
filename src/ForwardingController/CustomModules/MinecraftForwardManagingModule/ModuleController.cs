using CustomModule.Contract;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftForwardManagingModule
{
    public class ModuleController : ICustomModuleController
    {
        public string Name => "Minecraft Managing";
        public string IconKind => "Minecraft";

        public IDictionary<string, string> Configuration { get; set; }

        private bool _isRunning = false;
        public bool IsRunning => _isRunning;

        public List<string> TempLog { get; set; } = new List<string>();

        public void Run(IKernel kernel)
        {
            TempLog.Add("Test Logging Eintrag Start...");
            _isRunning = true;
        }

        public void Stop()
        {
            _isRunning = false;
            TempLog.Add("Test Logging Eintrag Stop...");
        }
    }
}