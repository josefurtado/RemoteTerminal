using BattleRoyalle.Service.Core.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using System.Diagnostics;

namespace BattleRoyalle.Service.Core
{
    public class PowerShellProcess : IPowerShellProcess
    {
        private Process _powerShell;
        private readonly HubConnection _hubConnection;

        public PowerShellProcess(HubConnection hubConnection)
        {
            _hubConnection = hubConnection;
        }

        public void OpenProcess()
        {
            _powerShell = new Process
            {
                StartInfo =
                {
                    WorkingDirectory = $@"C:\",
                    FileName = "powershell",
                    RedirectStandardInput = true,
                    ErrorDialog = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                }
            };

            ReadOutputData();

            _powerShell.Start();
            _powerShell.BeginOutputReadLine();
        }

        public void ExecuteCommand(string command)
        {
            _powerShell.StandardInput.WriteLine(command);
        }

        public void CloseProcess()
        {
            _powerShell.CancelOutputRead();
            _powerShell.Kill();
        }

        private void ReadOutputData()
        {
            _powerShell.OutputDataReceived += async (sender, outputLine) =>
            {
                if (outputLine.Data == null) return;

                await _hubConnection.InvokeAsync("SendResponseCommand", outputLine.Data);
            };
        }
    }
}
