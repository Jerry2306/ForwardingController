using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace DatabaseUtils.MySql
{
    public class MySqlDatabaseHelper : IDisposable
    {
        public ConnectionState State => _sqlCon.State;

        private MySqlConnection _sqlCon { get; set; }
        private MySqlCommand _command { get; set; }
        private MySqlDataReader _dataReader { get; set; }
        public MySqlDatabaseHelper(string connectionString)
        {
            _sqlCon = new MySqlConnection(connectionString);
        }

        public void Open()
        {
            try
            {
                _sqlCon.Open();
            }
            catch (Exception exc)
            {
                throw new Exception($"Database connection couldn't be opened: {exc.Message}", exc);
            }
        }

        public void Close()
        {
            try
            {
                _sqlCon.Close();
            }
            catch (Exception exc)
            {
                throw new Exception($"Database connection couldn't be closed: {exc.Message}", exc);
            }
        }

        public List<List<object>> RunQuery(string sql, int columnCnt)
        {
            if (string.IsNullOrEmpty(sql))
                throw new ArgumentException("Parameter 'sql' can't be empty!");

            var result = new List<List<object>>();

            try
            {
                if (State == ConnectionState.Closed)
                    Open();

                _command = new MySqlCommand(sql, _sqlCon);

                _dataReader = _command.ExecuteReader();

                while (_dataReader.Read())
                {
                    var cnt = result.Count;
                    result.Add(new List<object>());

                    for (var i = 0; i < columnCnt; i++)
                    {
                        result[cnt].Add(_dataReader.GetValue(i));
                    }
                }
                _dataReader.Close();
            }
            catch (Exception exc)
            {
                throw new Exception($"The query({sql}, {columnCnt}) couldn't be executed: {exc.Message}", exc);
            }

            return result;
        }

        public int RunNonQuery(string sql)
        {
            if (string.IsNullOrEmpty(sql))
                throw new ArgumentException("Parameter 'sql' can't be empty!");

            int result;
            try
            {
                if (State == ConnectionState.Closed)
                    Open();

                _command = new MySqlCommand(sql, _sqlCon);

                result = _command.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw new Exception($"The query({sql}) couldn't be executed: {exc.Message}", exc);
            }

            return result;
        }

        #region Dispose

        private bool _isDisposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isCalledByDispose)
        {
            if (_isDisposed)
                return;

            if (isCalledByDispose)
            {
                _sqlCon?.Close();
                _sqlCon?.Dispose();
                _sqlCon = null;

                _command?.Dispose();
                _command = null;

                _dataReader?.Close();
                _dataReader = null;
            }

            _isDisposed = true;
        }

        ~MySqlDatabaseHelper()
        {
            Dispose(false);
        }
        #endregion
    }
}