using BattleRoyalle.Shared.Entities;

namespace BattleRoyalle.Domain.Entities
{
    public class Disk : Entity
    {
        protected Disk() { }

        public Disk(string name, long freeDiskSpace, long totalDiskSize)
        {
            Name = name;
            FreeDiskSpace = freeDiskSpace;
            TotalDiskSize = totalDiskSize;
        }

        public string Name { get; private set; }
        public long FreeDiskSpace { get; private set; }
        public long TotalDiskSize { get; private set; }

        public Machine Machine { get; private set; }
    }
}
