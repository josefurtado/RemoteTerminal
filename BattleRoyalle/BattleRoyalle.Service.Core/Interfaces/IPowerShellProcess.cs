namespace BattleRoyalle.Service.Core.Interfaces
{
    public interface IPowerShellProcess
    {
        void OpenProcess();
        void CloseProcess();
        void ExecuteCommand(string command);
    }
}
