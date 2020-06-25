using BattleRoyalle.Service.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;

namespace BattleRoyalle.Service.Core
{
    public static class MachineSetup
    {
        public static string Name => Environment.MachineName;

        public static string OSVersion => Environment.OSVersion.VersionString;
        
        public static string IpAddress => GetIpAddress();

        public static MachineAntivirus Antivirus => GetAntivirus();
        
        public static IEnumerable<MachineDisk> Disks => GetDisks();
        
        private static string GetIpAddress()
        {
            return Dns.GetHostEntry(Dns.GetHostName())
                .AddressList
                .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork)?.ToString();
        }

        private static IEnumerable<MachineDisk> GetDisks()
        {
            return DriveInfo.GetDrives()
                .Select(x => new MachineDisk { 
                    Name = x.Name,
                    FreeDiskSpace = Math.Abs((x.TotalFreeSpace / 1024) / 1024),
                    TotalDiskSize = Math.Abs((x.TotalSize / 1024) / 1024)
                });
        }

        private static MachineAntivirus GetAntivirus()
        {
            string antivirus = String.Empty;

            ManagementObjectSearcher wmiData = new ManagementObjectSearcher(@"root\SecurityCenter2", "SELECT * FROM AntiVirusProduct");
            ManagementObjectCollection data = wmiData.Get();

            var machineAntivirus = new MachineAntivirus { 
                HasAntivirus = data.Count > 0           
            };

            foreach (ManagementObject virusChecker in data)
            {
                machineAntivirus.AntivirusName += String.Join(" ", virusChecker["displayName"].ToString());
            }

            return machineAntivirus;
        }
    }
}
