namespace ForwardingUtils.SharedItems.JsonModel.Entity
{
    public class HttpEntity
    {
        public decimal count { get; set; }
        public decimal rate1 { get; set; }
        public decimal rate5 { get; set; }
        public decimal rate15 { get; set; }
        public decimal p50 { get; set; }
        public decimal p90 { get; set; }
        public decimal p95 { get; set; }
        public decimal p99 { get; set; }

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