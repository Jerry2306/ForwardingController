using DatabaseUtils;
using DatabaseUtils.Contract;
using DatabaseUtils.SharedItems.Entities;
using Ninject.Modules;

namespace NinjectBindingManaging.Bindings
{
    public class DatabaseUtilsBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IDatabaseManager<NgrokTableEntity>>().To<MySqlDatabaseManager<NgrokTableEntity>>().InSingletonScope();
        }
    }
}