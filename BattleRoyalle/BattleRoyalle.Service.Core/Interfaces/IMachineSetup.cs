namespace BattleRoyalle.Service.Core.Interfaces
{
    public interface IMachineSetup
    {
        public string Name { get; }
        public string IpAddress { get; }
        public string Antivirus { get; }
        public string OSVersion { get; }
        public long FreeDiskSpace { get; }
        public long TotalDiskSize { get; }
    }
}
