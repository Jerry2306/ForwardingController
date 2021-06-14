namespace ForwardingUtils.SharedItems.JsonModel.Entity
{
    public class ConfigEntity
    {
        public string addr { get; set; }
        public bool inspect { get; set; }

        public ConfigEntity()
        {
            addr = string.Empty;
            inspect = false;
        }
    }
}