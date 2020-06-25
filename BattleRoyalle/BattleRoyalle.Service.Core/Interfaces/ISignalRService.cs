using System.Threading.Tasks;

namespace BattleRoyalle.Service.Core.Interfaces
{
    public interface ISignalRService
    {
        Task StartConnection();
        Task CloseConnection();
        Task SendMachineDetails();
    }
}
