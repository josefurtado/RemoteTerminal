using System.Collections.Generic;

namespace BattleRoyalle.Application.DTOs
{
    public class MachineDetailsDTO
    {
        public string Name { get; set; }
        public string OSVersion { get; set; }
        public string IpAddress { get; set; }
        public ICollection<DiskDTO> Disks { get; set; }
    }
}
