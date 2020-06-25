using BattleRoyalle.Service.Core.Interfaces;
using BattleRoyalle.Service.Core.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace BattleRoyalle.Service.Core
{
    public class SignalRService : ISignalRService
    {
        private readonly IPowerShellProcess _powerShell;
        private readonly HubConnection _hubConnection;

        public SignalRService(HubConnection hubConnection, IPowerShellProcess powerShell)
        {
            _hubConnection = hubConnection;
            _powerShell = powerShell;
        }

        public async Task CloseConnection()
        {
            await _hubConnection.StopAsync();
        }

        public async Task SendMachineDetails()
        {
            try
            {
                var machineDetails = new MachineDetails
                {
                    Name = MachineSetup.Name,
                    Disks = MachineSetup.Disks,
                    OSVersion = MachineSetup.OSVersion,
                    IpAddress = MachineSetup.IpAddress
                };

                var machineDetailsStr = JsonSerializer.Serialize(machineDetails);

                await _hubConnection.InvokeAsync("SendMachineDetails", machineDetailsStr);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task StartConnection()
        {
            try
            {
                await _hubConnection.StartAsync();

                BuildHandlers();
            }
            catch (Exception)
            {

                throw;
            }
        }



        private void BuildHandlers()
        {
            _hubConnection.On<string>("HandShake", (machine) =>
            {
                Console.WriteLine(machine);
            });

            _hubConnection.On("OpenTerminal", () =>
            {
                _powerShell.OpenProcess();
            });

            _hubConnection.On("CloseTerminal", () =>
            {
                _powerShell.CloseProcess();
            });

            _hubConnection.On<string>("ExecuteCommand", (command) =>
            {
                _powerShell.ExecuteCommand(command);
            });

            _hubConnection.Reconnected += async (args) =>
            {
                await _hubConnection.InvokeAsync("SendReconnected", "");
            };
        }

    }
}
