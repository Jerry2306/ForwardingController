namespace ForwardingUtils.SharedItems.JsonModel.Entity
{
    public class MetricsEntity
    {
        public ConnsEntity conns { get; set; }
        public HttpEntity http { get; set; }

        public MetricsEntity()
        {
            conns = new ConnsEntity();
            http = new HttpEntity();
        }
    }
}