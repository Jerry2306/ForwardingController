using DatabaseUtils.Contract;
using DatabaseUtils.MySql;
using DatabaseUtils.SharedItems.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseUtils.Wrapper
{
    public class NgrokTableEntityWrapper : IDatabaseManager<NgrokTableEntity>
    {
        private readonly MySqlDatabaseHelper _helper;
        private readonly string _tableName;
        public NgrokTableEntityWrapper(string cs, string tableName)
        {
            _helper = new MySqlDatabaseHelper(cs);
            _tableName = tableName;
        }

        public IEnumerable<NgrokTableEntity> GetAll()
        {
            var dbResult = _helper.RunQuery($"SELECT * FROM {_tableName}", 3);

            var result = new List<NgrokTableEntity>();

            dbResult.ForEach(x => result.Add(new NgrokTableEntity(x[0].ToString(), x[1].ToString(), DateTime.Parse(x[2].ToString()))));

            return result;
        }

        public IEnumerable<NgrokTableEntity> GetAll(Func<NgrokTableEntity, bool> predicate)
        {
            var dbResult = _helper.RunQuery($"SELECT * FROM {_tableName}", 3);

            var result = new List<NgrokTableEntity>();

            dbResult.ForEach(x => result.Add(new NgrokTableEntity(x[0].ToString(), x[1].ToString(), DateTime.Parse(x[2].ToString()))));

            return result.Where(predicate);
        }

        public NgrokTableEntity Insert(NgrokTableEntity e)
        {
            var dbResult = _helper.RunNonQuery($"INSERT INTO {_tableName} (ConnectionName, ConnectionAddress, ConnectionDate) VALUES ('{e.ConnectionName}','{e.ConnectionAddress}','{e.ConnectionDate:yyyy-MM-dd HH:mm:ss}')");
            if (dbResult != 1)
                throw new ArgumentException($"Entity couldn't be inserted: {e.ConnectionName}, {e.ConnectionAddress}, {e.ConnectionDate:yyyy-MM-dd HH:mm:ss}");

            return e;
        }

        public void Update(NgrokTableEntity e)
        {
            if (GetAll().All(x => x.ConnectionName != e.ConnectionName))
                throw new ArgumentException($"ConnectionName is the pk. {e.ConnectionName} wasn't found in table!");

            _helper.RunNonQuery($"UPDATE {_tableName} SET ConnectionAddress = '{e.ConnectionAddress}', ConnectionDate = '{e.ConnectionDate:yyyy-MM-dd HH:mm:ss}' WHERE ConnectionName = '{e.ConnectionName}'");
        }

        public void Delete(NgrokTableEntity e)
        {
            _helper.RunNonQuery($"DELETE FROM {_tableName} WHERE ConnectionName = '{e.ConnectionName}'");
        }
    }
}