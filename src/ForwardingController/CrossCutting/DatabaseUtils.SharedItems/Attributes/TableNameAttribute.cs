using System;

namespace DatabaseUtils.SharedItems.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute : Attribute
    {
        private string _tableName;
        public TableNameAttribute(string tableName)
        {
            _tableName = tableName;
        }

        public string GetTableName() => _tableName;
    }
}