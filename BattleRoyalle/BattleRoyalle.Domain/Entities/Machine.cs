using BattleRoyalle.Shared.Entities;
using System.Collections.Generic;

namespace BattleRoyalle.Domain.Entities
{
    public class Machine : Entity
    {
        protected Machine() {}

        public Machine(string name, string osversion, string ipAddress, string connectionId = null)
        {
            Name = name;
            OSVersion = osversion;
            IpAddress = ipAddress;
            Disks = new List<Disk>();
            LastConnectionId = connectionId;
        }

        public string Name { get; private set; }
        public string OSVersion { get; private set; }
        public string IpAddress { get; private set; }
        public string LastConnectionId { get; private set; }
        public ICollection<Disk> Disks { get; private set; }

        public void AddDisk(Disk disk)
        {
            Disks.Add(disk);
        }
    }
}
