using BattleRoyalle.Application.DTOs;
using BattleRoyalle.Domain.Commands.Input.Machine;
using BattleRoyalle.Domain.Entities;
using BattleRoyalle.Domain.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BattleRoyalle.Api.SignalR
{
    public class BattleRoyalleHub : Hub<ITypedHub>
    {
        private readonly IMachineService _machineService;

        public BattleRoyalleHub(IMachineService machineService)
        {
            _machineService = machineService;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var machine = _machineService.GetMachineByConnectionId(connectionId: ConnectionId);

            var result = _machineService.DeleteMachine(machineId: machine.Id);

            if (result.Success)
                UpdateMachineList();

            return base.OnDisconnectedAsync(exception);
        }

        public void SendMachineDetails(string machineDetails)
        {
            var machine = JsonSerializer.Deserialize<MachineDetailsDTO>(machineDetails);

            var command = new CreateMachineCommand { 
                Name         = machine.Name,
                OSVersion    = machine.OSVersion,
                IpAddress    = machine.IpAddress,
                ConnectionId = ConnectionId,
                HasAntivirus = machine.HasAntivirus,
                AntivirusName = machine.AntivirusName,
                Disks        = machine.Disks.Select(x => 
                                                    new Disk(
                                                        name: x.Name, 
                                                        freeDiskSpace: x.FreeDiskSpace, 
                                                        totalDiskSize: x.TotalDiskSize)
                                                   )
                                            .ToList()
            };

            var result = _machineService.SaveMachine(command: command);

            if (result.Success)
                UpdateMachineList();
        }

        public Task SendResponseCommand(string response)
        {
            return Clients.All.ShowResponseCommand(response);
        }

        public Task ExecuteTerminalCommand(string command, string connectionId)
        {
            return Clients.Client(connectionId).ExecuteCommand(command);
        }

        public Task OpenTerminal(string connectionId)
        {
            return Clients.Client(connectionId).OpenTerminal();
        }

        public Task CloseTerminal(string connectionId)
        {
            return Clients.Client(connectionId).CloseTerminal();
        }

        public Task UpdateMachineList()
        {
            return Clients.All.UpdateMachineList(update: true);
        }

        private string ConnectionId => Context.ConnectionId;
    }
}
