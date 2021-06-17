using CustomModule.Contract;
using Ninject;
using System.Collections.Generic;
using System.Reflection;

namespace CustomModuleUtils.Contract
{
    public interface ICustomModuleManager
    {
        IDictionary<Assembly, ICustomModuleController> CustomModules { get; }
        void RunCustomModules(IKernel kernel);
        void StopCustomModules();
    }
}