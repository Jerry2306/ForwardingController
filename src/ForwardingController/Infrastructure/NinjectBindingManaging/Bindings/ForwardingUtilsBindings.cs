using ForwardingUtils;
using ForwardingUtils.Contract;
using Ninject.Modules;

namespace NinjectBindingManaging.Bindings
{
    public class ForwardingUtilsBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IForwardManager>().To<ForwardManager>();
        }
    }
}