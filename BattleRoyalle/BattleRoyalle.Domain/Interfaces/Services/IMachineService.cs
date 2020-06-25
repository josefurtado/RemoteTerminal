using BattleRoyalle.Domain.Commands.Input.Machine;
using BattleRoyalle.Domain.Entities;
using BattleRoyalle.Shared.Commands;
using System;
using System.Collections.Generic;

namespace BattleRoyalle.Domain.Interfaces.Services
{
    public interface IMachineService
    {
        Machine GetMachine(Guid id);
        Machine GetMachineByConnectionId(string connectionId);
        IEnumerable<Machine> GetMachines();
        ICommandResult SaveMachine(CreateMachineCommand command);
        ICommandResult DeleteMachine(Guid machineId);
    }
}
