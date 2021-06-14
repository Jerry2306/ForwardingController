using ForwardingUtils.SharedItems.JsonModel.Request;
using ForwardingUtils.SharedItems.JsonModel.Response;

namespace ForwardingUtils.Contract
{
    public interface IForwardManager
    {
        TunnelEntity StartTunnel(StartTunnelEntity entity);
        void StopTunnel(string name);
        TunnelListEntity ListTunnels();
        TunnelEntity GetTunnel(string name);
    }
}