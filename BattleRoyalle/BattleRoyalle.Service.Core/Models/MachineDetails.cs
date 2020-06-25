using System.Collections.Generic;

namespace BattleRoyalle.Service.Core.Models
{
    public class MachineDetails
    {
        public string Name { get; set; }
        public string OSVersion { get; set; }
        public string IpAddress { get; set; }
        public bool HasAntivirus { get; set; }
        public string AntivirusName { get; set; }
        public IEnumerable<MachineDisk> Disks { get; set; }
    }
}
