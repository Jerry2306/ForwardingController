using System;
using DatabaseUtils.SharedItems.Attributes;

namespace DatabaseUtils.SharedItems.Entities
{
    [TableName("ngrok")]
    public class NgrokTableEntity : BaseDatabaseEntity
    {
        public string ConnectionName { get; set; }
        public string ConnectionAddress { get; set; }
        public DateTime ConnectionDate { get; set; }

        public NgrokTableEntity() { }

        public NgrokTableEntity(string connectionName, string connectionAddress, DateTime connectionDate)
        {
            ConnectionName = connectionName;
            ConnectionAddress = connectionAddress;
            ConnectionDate = connectionDate;
        }
    }
}