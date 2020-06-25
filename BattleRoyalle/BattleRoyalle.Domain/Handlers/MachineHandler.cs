using BattleRoyalle.Domain.Commands.Input.Machine;
using BattleRoyalle.Domain.Commands.Output;
using BattleRoyalle.Domain.Entities;
using BattleRoyalle.Domain.Interfaces.UnitOfWork;
using BattleRoyalle.Shared.Commands;
using System;

namespace BattleRoyalle.Domain.Handlers
{
    public class MachineHandler :
        ICommandHandler<CreateMachineCommand>,
        ICommandHandler<DeleteMachineCommand>
    {
        private readonly IUnitOfWork _uow;

        public MachineHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ICommandResult Handle(CreateMachineCommand command)
        {
            try
            {
                var machine = new Machine(
                    name: command.Name, 
                    osversion: command.OSVersion, 
                    hasAntivirus: command.HasAntivirus, 
                    antivirusName: command.AntivirusName, 
                    ipAddress: command.IpAddress, 
                    connectionId: command.ConnectionId);

                foreach (var disk in command.Disks)
                {
                    machine.AddDisk(disk);
                }

                _uow.GetRepository<Machine>().Save(machine);

                _uow.SaveChanges();

                return new CommandResult(success: true, message: "Máquina salva com sucesso", data: command);
            }
            catch (Exception ex)
            {
                return new CommandResult(success: false, message: ex.Message, data: null);
            }

        }

        public ICommandResult Handle(DeleteMachineCommand command)
        {
            try
            {
                _uow.GetRepository<Machine>().Delete(command.MachineId);

                _uow.SaveChanges();

                return new CommandResult(success: true, message: "Máquina removida com sucesso", data: command);
            }
            catch (Exception ex)
            {
                return new CommandResult(success: false, message: ex.Message, data: null);
            }
        }
    }
}
