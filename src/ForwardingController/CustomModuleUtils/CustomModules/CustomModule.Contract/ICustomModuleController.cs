using Ninject;
using System.Collections.Generic;

namespace CustomModule.Contract
{
    public interface ICustomModuleController
    {
        IDictionary<string, string> Configuration { get; set; }
        void Run(IKernel kernel);
        void Stop();
    }
}