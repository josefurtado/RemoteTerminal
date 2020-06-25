using BattleRoyalle.Service.Core.Interfaces;
using System.Threading.Tasks;

namespace BattleRoyalle.Service
{
    public class Startup
    {
        private readonly ISignalRService _signalRService;

        public Startup(ISignalRService signalRService)
        {
            _signalRService = signalRService;
        }

        public void StartServices()
        {
            Task.Run(async () =>
            {
                await _signalRService.StartConnection();

                await _signalRService.SendMachineDetails();
            });
        }

        public void StopServices()
        {
            _signalRService.CloseConnection();
        }
    }
}
