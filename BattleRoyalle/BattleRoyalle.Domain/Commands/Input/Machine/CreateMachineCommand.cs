using BattleRoyalle.Domain.Entities;
using BattleRoyalle.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System.Collections.Generic;

namespace BattleRoyalle.Domain.Commands.Input.Machine
{
    public class CreateMachineCommand : Notifiable, ICommand
    {

        public CreateMachineCommand()
        {
            Disks = new List<Disk>();
        }

        public string Name { get; set; }
        public string OSVersion { get; set; }
        public string IpAddress { get; set; }
        public string ConnectionId { get; set; }
        public string AntivirusName { get; set; }
        public bool HasAntivirus { get; set; }

        public ICollection<Disk> Disks { get; set; }

        public bool Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(Name, "Name", "O Nome da máquina deve ser informado.")
                .IsNotNullOrEmpty(OSVersion, "OSVersion", "A versão do sistema operacional deve ser informada.")
                .IsNotNullOrEmpty(IpAddress, "IpAddress", "O IP da máquina deve ser informado.")
                .IsTrue(Disks.Count > 0, "Disks", "Pelo menos um Hd deve ser informado.")
                );

            return Valid;
        }
    }
}
