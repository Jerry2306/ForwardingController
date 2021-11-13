using DatabaseUtils.Contract;
using DatabaseUtils.SharedItems.Entities;
using ForwardingUtils.Contract;
using ForwardingUtils.SharedItems.JsonModel.Request;
using ForwardingUtils.SharedItems.JsonModel.Response;
using Ninject;
using System;
using System.Linq;

namespace MinecraftForwardManagingModule
{
    public class MinecraftForwardRefresher
    {
        private readonly ModuleController _moduleController;
        private readonly IForwardManager _forwardManager;
        private readonly IKernel _kernel;

        private IDatabaseManager<NgrokTableEntity> _databaseManager;
        public MinecraftForwardRefresher(ModuleController moduleController, IKernel kernel)
        {
            _moduleController = moduleController;
            _forwardManager = kernel.Get<IForwardManager>();
            _databaseManager = kernel.Get<IDatabaseManager<NgrokTableEntity>>();
            _kernel = kernel;
        }

        public void RefreshCycle()
        {
            var serverPort = _moduleController.Configuration["MinecraftServerPort"];
            var forwardName = _moduleController.Configuration["MinecraftForwardName"];
            TunnelEntity currentTunnel = null;
            try
            {
                var tunnels = _forwardManager.ListTunnels().tunnels;
                if (!tunnels.Any(x => x.name == forwardName))
                    currentTunnel = _forwardManager.StartTunnel(new StartTunnelEntity(serverPort, "tcp", forwardName));
                else
                    currentTunnel = tunnels.FirstOrDefault(x => x.name == forwardName);

                if (currentTunnel == null)
                    throw new ArgumentNullException(nameof(currentTunnel), "Current tunnel couldn't be fetched!");
            }
            catch (Exception e)
            {
                _moduleController.TempLog.Add("Error at fetching tunnels: " + e.Message);
                return;
            }

            try
            {
                var tunnelEntity = _databaseManager.GetAll(x => x.ConnectionName == forwardName).FirstOrDefault();

                if (tunnelEntity != null)
                {
                    tunnelEntity.ConnectionAddress = currentTunnel.public_url;
                    tunnelEntity.ConnectionDate = DateTime.Now;
                    _databaseManager.Update(tunnelEntity);
                }
                else
                    _databaseManager.Insert(new NgrokTableEntity(currentTunnel.name, currentTunnel.public_url, DateTime.Now));
            }
            catch (Exception e)
            {
                _moduleController.TempLog.Add("Error at fetching database (trying new instancing of database manager): " + e.Message);
                _databaseManager = _kernel.Get<IDatabaseManager<NgrokTableEntity>>();
            }
        }

        public void TryStopForward()
        {
            var forwardName = _moduleController.Configuration["MinecraftForwardName"];
            try
            {
                var currentTunnel = _forwardManager.ListTunnels().tunnels?.FirstOrDefault(x => x.name == forwardName); ;

                if (currentTunnel != null)
                    _forwardManager.StopTunnel(currentTunnel.name);
            }
            catch (Exception e)
            {
                _moduleController.TempLog.Add("Error at stopping mc tunnel: " + e.Message);
            }

            try
            {
                var tunnelEntity = _databaseManager.GetAll(x => x.ConnectionName == forwardName).FirstOrDefault();

                if (tunnelEntity != null)
                {
                    tunnelEntity.ConnectionAddress = string.Empty;
                    _databaseManager.Update(tunnelEntity);
                }
            }
            catch (Exception e)
            {
                _moduleController.TempLog.Add("Error at clearing database entry: " + e.Message);
            }
        }
    }
}