using BattleRoyalle.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace BattleRoyalle.Domain.Commands.Input.Machine
{
    public class DeleteMachineCommand : Notifiable, ICommand
    {
        public Guid MachineId { get; set; }

        public bool Validate()
        {
            AddNotifications(new Contract()
                .IsTrue(MachineId != Guid.Empty, "MachineId", "O Id da máquina deve ser informado.")
                );

            return Valid;
        }
    }
}
