using BattleRoyalle.Domain.Commands.Input.Machine;
using BattleRoyalle.Domain.Commands.Output;
using BattleRoyalle.Domain.Entities;
using BattleRoyalle.Domain.Handlers;
using BattleRoyalle.Domain.Interfaces.Services;
using BattleRoyalle.Domain.Interfaces.UnitOfWork;
using BattleRoyalle.Shared.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BattleRoyalle.Application.Services
{
    public class MachineService : IMachineService
    {
        private readonly IUnitOfWork _uow;
        private readonly MachineHandler _handler;

        public MachineService(IUnitOfWork uow, MachineHandler handler)
        {
            _uow = uow;
            _handler = handler;
        }

        public Machine GetMachine(Guid id)
        {
            return _uow
                .GetRepository<Machine>()
                .GetFirstOrDefault(predicate: x => x.Id == id, include: x => x.Include(y => y.Disks), disableTracking: true);
        }

        public IEnumerable<Machine> GetMachines()
        {
            return _uow
                .GetRepository<Machine>()
                .Get(include: x => x.Include(y => y.Disks));
        }
        public ICommandResult DeleteMachine(Guid machineId)
        {
            var command = new DeleteMachineCommand { 
                MachineId = machineId
            };

            command.Validate();

            if (!command.Valid)
                return new CommandResult(success: false, message: null, data: command.Notifications);

            return _handler.Handle(command);
        }

        public ICommandResult SaveMachine(CreateMachineCommand command)
        {
            command.Validate();

            if (!command.Valid)
                return new CommandResult(success: false, message: null, data: command.Notifications);

            return _handler.Handle(command);
        }

        public Machine GetMachineByConnectionId(string connectionId)
        {
            return _uow
                .GetRepository<Machine>()
                .GetFirstOrDefault(predicate: x => x.LastConnectionId.Equals(connectionId), include: x => x.Include(y => y.Disks), disableTracking: true);
        }
    }
}
