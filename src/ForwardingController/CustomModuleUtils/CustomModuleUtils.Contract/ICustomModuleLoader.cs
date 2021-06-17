using CustomModule.Contract;
using System.Collections.Generic;
using System.Reflection;

namespace CustomModuleUtils.Contract
{
    public interface ICustomModuleLoader
    {
        IDictionary<Assembly, ICustomModuleController> GetCustomModules();
    }
}