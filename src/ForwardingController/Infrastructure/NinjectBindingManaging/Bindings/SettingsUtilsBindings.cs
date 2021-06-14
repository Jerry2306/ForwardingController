using Ninject.Modules;
using SettingsUtils;
using SettingsUtils.Contract;

namespace NinjectBindingManaging.Bindings
{
    public class SettingsUtilsBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ISettingsManager>().To<SettingsManager>().InSingletonScope();
        }
    }
}