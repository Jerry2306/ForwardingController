using CustomModuleUtils;
using CustomModuleUtils.Contract;
using Ninject.Modules;

namespace NinjectBindingManaging.Bindings
{
    public class CustomModuleUtilsBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ICustomModuleLoader>().To<CustomModuleLoader>();
            Bind<ICustomModuleManager>().To<CustomModuleManager>().InSingletonScope();
        }
    }
}