using Ninject;
using Ninject.Modules;
using NinjectBindingManaging.Bindings;
using System;

namespace NinjectBindingManaging
{
    public class NinjectBindingManager
    {
        public INinjectModule[] Bindings { get; }

        private static IKernel _kernelInstance;

        public NinjectBindingManager()
        {
            try
            {
                Bindings = new INinjectModule[]
                {
                    new SettingsUtilsBindings(),
                    new ForwardingUtilsBindings()
                };
            }
            catch (Exception exc)
            {
                throw new Exception("Error while defining ninject bindings!", exc);
            }
        }

        public IKernel GetKernel()
        {
            _kernelInstance = _kernelInstance ?? new StandardKernel(Bindings);
            return _kernelInstance;
        }
    }
}