namespace BattleRoyalle.Shared.Commands
{
    public interface ICommandResult
    {
        object Data { get; set; }
        bool Success { get; set; }
        string Message { get; set; }
    }
}
