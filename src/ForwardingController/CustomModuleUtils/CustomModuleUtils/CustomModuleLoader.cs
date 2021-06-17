using CustomModule.Contract;
using CustomModuleUtils.Contract;
using CustomModuleUtils.Helper;
using SettingsUtils.Contract;
using SettingsUtils.SharedItems.ConfigurationEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CustomModuleUtils
{
    public class CustomModuleLoader : ICustomModuleLoader
    {
        private readonly AppSettings _settings;
        private readonly CustomModuleConfigurationSplitter _configSplitter;
        public CustomModuleLoader(ISettingsManager settingsManager)
        {
            _settings = settingsManager.Get<AppSettings>();
            _configSplitter = new CustomModuleConfigurationSplitter();
        }

        public IDictionary<Assembly, ICustomModuleController> GetCustomModules()
        {
            var assemblies = GetAssemblies();
            var result = new Dictionary<Assembly, ICustomModuleController>();

            foreach (var assembly in assemblies)
            {
                var controllerInstance = GetController(assembly);
                controllerInstance.Configuration = new Dictionary<string, string>();

                var configForAssembly = new FileInfo(
                    Path.Combine(_settings.CustomModulesConfiguration.CustomModulesFolder, Path.GetFileNameWithoutExtension(assembly.ManifestModule.ScopeName)) + ".Configuration");

                if (configForAssembly.Exists)
                    controllerInstance.Configuration = _configSplitter.GetConfiguration(configForAssembly);

                result.Add(assembly, controllerInstance);
            }

            return result;
        }

        private IEnumerable<Assembly> GetAssemblies()
        {
            var modulesDir = new DirectoryInfo(_settings.CustomModulesConfiguration.CustomModulesFolder);
            if (!modulesDir.Exists)
                throw new DirectoryNotFoundException($"Couldn't load custom modules because the directory '{modulesDir.FullName}' wasn't found!"); //TODO: Log instead of exception if logger is implemented and LoadCustomModules is true

            var files = modulesDir.GetFiles("*.dll").ToList();
            var assemblyFiles = new List<Assembly>();

            files.ForEach(x => assemblyFiles.Add(Assembly.Load(File.ReadAllBytes(x.FullName))));

            return assemblyFiles;
        }

        private ICustomModuleController GetController(Assembly assembly)
        {
            var controllerList = assembly.GetTypes().Where(x => x.GetInterfaces().Contains(typeof(ICustomModuleController))).ToList();
            if (controllerList.Count != 1)
                throw new Exception($"Couldn't find specific ICustomModuleController type in module '{assembly.FullName}'!");//TODO: loggen und einfach nicht laden

            var controller = controllerList.First();
            if (!controller.GetConstructors().Any(x => x.GetParameters().Length == 0))
                throw new Exception("Custom module constructor can't have any parameters. Use IKernel in Run method to access the instances!"); //TODO: loggen und einfach nicht laden

            return (ICustomModuleController)Activator.CreateInstance(controller);
        }
    }
}