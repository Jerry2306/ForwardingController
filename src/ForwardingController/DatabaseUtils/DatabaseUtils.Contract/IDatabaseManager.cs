using System;
using System.Collections.Generic;
using DatabaseUtils.SharedItems.Entities;

namespace DatabaseUtils.Contract
{
    public interface IDatabaseManager<TEntity> where TEntity : BaseDatabaseEntity
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate);
        TEntity Insert(TEntity e);
        void Update(TEntity e);
        void Delete(TEntity e);
    }
}