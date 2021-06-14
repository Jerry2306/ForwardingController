namespace ForwardingUtils.SharedItems.JsonModel.Request
{
    public class StartTunnelEntity
    {
        public string addr { get; set; }
        public string proto { get; set; }
        public string name { get; set; }

        public StartTunnelEntity()
        {
            addr = string.Empty;
            proto = string.Empty;
            name = string.Empty;
        }

        public StartTunnelEntity(string addr, string proto, string name)
        {
            this.addr = addr;
            this.proto = proto;
            this.name = name;
        }
    }
}