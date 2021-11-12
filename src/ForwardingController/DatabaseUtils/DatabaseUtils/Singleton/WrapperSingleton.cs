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
        public static IDatabaseManager<TEntity> GetInstance(string cs)
        {
            lock (_instanceLock)
            {
                return SwitchInstances(cs);
            }
        }

        private static IDatabaseManager<TEntity> SwitchInstances(string cs)
        {
            var tableName = (Attribute.GetCustomAttribute(typeof(TEntity), typeof(TableNameAttribute)) as TableNameAttribute)?.GetTableName();

            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException($"TableNameAttribute must be declared in database entities! Entity: {nameof(TEntity)}");

            if (typeof(TEntity) == typeof(NgrokTableEntity))
                return (IDatabaseManager<TEntity>)new NgrokTableEntityWrapper(cs, tableName);

            throw new InstanceNotFoundException($"Instance for type '{typeof(TEntity).Name}' is not implemented in database wrapper helper!");
        }
    }
}