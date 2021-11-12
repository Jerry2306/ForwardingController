using DatabaseUtils.Contract;
using DatabaseUtils.SharedItems.Entities;
using DatabaseUtils.Singleton;
using SettingsUtils.Contract;
using SettingsUtils.SharedItems.ConfigurationEntities;
using System;
using System.Collections.Generic;

namespace DatabaseUtils
{
    public class MySqlDatabaseManager<TEntity> : IDatabaseManager<TEntity> where TEntity : BaseDatabaseEntity
    {
        private readonly string _cs;
        private readonly IDatabaseManager<TEntity> _entityWrapperInstance;
        public MySqlDatabaseManager(ISettingsManager settingsManager)
        {
            _cs = settingsManager.Get<AppSettings>().DatabaseConfiguration.ConnectionString;
            _entityWrapperInstance = WrapperSingleton<TEntity>.GetInstance(_cs);
        }

        public IEnumerable<TEntity> GetAll() => _entityWrapperInstance.GetAll();

        public IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate) => _entityWrapperInstance.GetAll(predicate);

        public TEntity Insert(TEntity e) => _entityWrapperInstance.Insert(e);

        public void Update(TEntity e) => _entityWrapperInstance.Update(e);

        public void Delete(TEntity e) => _entityWrapperInstance.Delete(e);
    }
}