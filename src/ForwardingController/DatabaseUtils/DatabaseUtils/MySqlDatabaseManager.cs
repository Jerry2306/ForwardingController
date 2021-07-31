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
        public MySqlDatabaseManager(ISettingsManager settingsManager)
        {
            _cs = settingsManager.Get<AppSettings>().DatabaseConfiguration.ConnectionString;
        }

        public IEnumerable<TEntity> GetAll() => WrapperSingleton<TEntity>.GetInstance(_cs).GetAll();

        public IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate) => WrapperSingleton<TEntity>.GetInstance(_cs).GetAll(predicate);

        public TEntity Insert(TEntity e) => WrapperSingleton<TEntity>.GetInstance(_cs).Insert(e);

        public void Update(TEntity e) => WrapperSingleton<TEntity>.GetInstance(_cs).Update(e);

        public void Delete(TEntity e) => WrapperSingleton<TEntity>.GetInstance(_cs).Delete(e);
    }
}