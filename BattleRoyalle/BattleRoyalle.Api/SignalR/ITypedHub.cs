using System.Threading.Tasks;

namespace BattleRoyalle.Api.SignalR
{
    public interface ITypedHub
    {
        Task OpenTerminal();
        Task CloseTerminal();
        Task ExecuteCommand(string command);
        Task UpdateMachineList(bool update);
        Task ShowResponseCommand(string response);
    }
}
