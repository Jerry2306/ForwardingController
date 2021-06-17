using CustomModule.Contract;
using CustomModuleUtils.Contract;
using Ninject;
using SettingsUtils.Contract;
using SharedItems.ConfigurationEntities;
using SharedItems.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CustomModuleUtils
{
    public class CustomModuleManager : ICustomModuleManager
    {
        public IDictionary<Assembly, ICustomModuleController> CustomModules { get; }

        private readonly AppSettings _settings;
        public CustomModuleManager(ISettingsManager settingsManager, ICustomModuleLoader loader)
        {
            _settings = settingsManager.Get<AppSettings>();

            if (_settings.CustomModulesConfiguration.LoadCustomModules)
                CustomModules = loader.GetCustomModules();
        }

        public void RunCustomModules(IKernel kernel)
        {
            if (!_settings.CustomModulesConfiguration.LoadCustomModules)
                return;

            foreach (var customModule in CustomModules)
            {
                if (customModule.Value == null)
                    throw new Exception($"Couldn't find controller in module '{customModule.Key.FullName}'!");

                CheckForTimeout(Task.Run(() => customModule.Value.Run(kernel)), customModule, "running");
            }
        }

        public void StopCustomModules() => 
            CustomModules.ToList().ForEach(x => CheckForTimeout(Task.Run(() => x.Value?.Stop()), x, "stopping"));

        private void CheckForTimeout(Task t, KeyValuePair<Assembly, ICustomModuleController> module, string source)
        {
            var start = DateTime.Now;

            var sleepInterval = 10;
            var timeout = false;
            while (!t.IsCompleted && !t.IsFaulted && !timeout)
            {
                Thread.Sleep(sleepInterval);

                if (start.AddSeconds(_settings.CustomModulesConfiguration.ModuleStopTimeout) <= DateTime.Now)
                    timeout = true;
            }

            if (timeout)
                throw new TimeoutException($"While {source} the controller in module '{module.Key.FullName}' a timeout occured!");//TODO: logging
            if (t.IsFaulted)
                throw new Exception($"While {source} the controller in module '{module.Key.FullName}' an error occured: {t.Exception.GetFormattedMessage()}");

            //else log successful
        }
    }
}