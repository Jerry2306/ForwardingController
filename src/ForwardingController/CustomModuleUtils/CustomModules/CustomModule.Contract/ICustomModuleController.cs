using Ninject;
using System.Collections.Generic;

namespace CustomModule.Contract
{
    public interface ICustomModuleController
    {
        string IconKind { get; }
        string Name { get; }
        IDictionary<string, string> Configuration { get; set; }
        bool IsRunning { get; }
        void Run(IKernel kernel);
        void Stop();

        //temporary - replace with logging framework and wrapper inside kernel -> module registers with name -> logs can be get by module name
        List<string> TempLog { get; set; }
    }
}