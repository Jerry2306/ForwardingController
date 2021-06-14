using System.Collections.Generic;

namespace ForwardingUtils.SharedItems.JsonModel.Response
{
    public class TunnelListEntity
    {
        public List<TunnelEntity> tunnels { get; set; }

        public TunnelListEntity()
        {
            tunnels = new List<TunnelEntity>();
        }
    }
}