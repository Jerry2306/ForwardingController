namespace ForwardingUtils.SharedItems.JsonModel.Entity
{
    public class HttpEntity
    {
        public int count { get; set; }
        public int rate1 { get; set; }
        public int rate5 { get; set; }
        public int rate15 { get; set; }
        public int p50 { get; set; }
        public int p90 { get; set; }
        public int p95 { get; set; }
        public int p99 { get; set; }

        public HttpEntity()
        {
            count = 0;
            rate1 = 0;
            rate5 = 0;
            rate15 = 0;
            p50 = 0;
            p90 = 0;
            p95 = 0;
            p99 = 0;
        }
    }
}