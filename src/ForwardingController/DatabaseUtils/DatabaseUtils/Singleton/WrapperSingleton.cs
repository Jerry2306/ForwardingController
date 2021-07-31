using DatabaseUtils.Contract;
using DatabaseUtils.SharedItems.Attributes;
using DatabaseUtils.SharedItems.Entities;
using DatabaseUtils.Wrapper;
using System;
using System.Management.Instrumentation;

namespace DatabaseUtils.Singleton
{
    public static class WrapperSingleton<TEntity> where TEntity : BaseDatabaseEntity
    {
        private static readonly object _instanceLock = new object();

        private static NgrokTableEntityWrapper _ngrokTableEntityWrapperInstance = null;

        public static IDatabaseManager<TEntity> GetInstance(string cs)
        {
            lock (_instanceLock)
            {
                return SwitchInstances(cs);
            }
        }

        private static IDatabaseManager<TEntity> SwitchInstances(string cs)
        {
            var tableName = (Attribute.GetCustomAttribute(typeof(NgrokTableEntity), typeof(TableNameAttribute)) as TableNameAttribute)?.GetTableName();
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException($"TableNameAttribute must be declared in database entities! Entity: {nameof(NgrokTableEntity)}");

            if (typeof(TEntity) == typeof(NgrokTableEntity))
            {
                _ngrokTableEntityWrapperInstance = _ngrokTableEntityWrapperInstance ?? new NgrokTableEntityWrapper(cs, tableName);
                return (IDatabaseManager<TEntity>)_ngrokTableEntityWrapperInstance;
            }

            throw new InstanceNotFoundException($"Instance for type '{typeof(TEntity).Name}' is not implemented in database signleton!");
        }
    }
}