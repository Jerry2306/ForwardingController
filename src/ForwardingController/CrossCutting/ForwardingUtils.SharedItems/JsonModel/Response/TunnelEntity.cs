using ForwardingUtils.SharedItems.JsonModel.Entity;

namespace ForwardingUtils.SharedItems.JsonModel.Response
{
    public class TunnelEntity
    {
        public string name { get; set; }
        public string uri { get; set; }
        public string public_url { get; set; }
        public string proto { get; set; }
        public ConfigEntity config { get; set; }
        public MetricsEntity metrics { get; set; }

        public TunnelEntity()
        {
            name = string.Empty;
            uri = string.Empty;
            public_url = string.Empty;
            proto = string.Empty;

            config = new ConfigEntity();
            metrics = new MetricsEntity();
        }
    }
}