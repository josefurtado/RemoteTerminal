using System.Collections.Generic;

namespace BattleRoyalle.Application.DTOs
{
    public class MachineDetailsDTO
    {
        public string Name { get; set; }
        public string OSVersion { get; set; }
        public string IpAddress { get; set; }
        public string AntivirusName { get; set; }
        public bool HasAntivirus { get; set; }
        public ICollection<DiskDTO> Disks { get; set; }
    }
}
