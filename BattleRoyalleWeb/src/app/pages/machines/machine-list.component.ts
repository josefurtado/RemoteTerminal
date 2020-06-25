import { Component, OnInit } from "@angular/core";
import * as signalR from '@microsoft/signalr';
import { MachineService } from './machine.service';
import { MachineModel } from 'src/app/models/machine.model';
import { FormControl, Validators } from '@angular/forms';

const connection = new signalR.HubConnectionBuilder()
  .withUrl(`http://localhost:50198/battleRoyalleHub`, {
    transport: signalR.HttpTransportType.LongPolling
  })
  .build();

@Component({
  selector: "app-machine-list",
  templateUrl: "machine-list.component.html"
})
export class MachineListComponent implements OnInit {
  view: string = 'list';
  machines: MachineModel[] = [];
  selectedMachine: MachineModel;
  terminalResponse: string[] = [];
  terminalCommand: FormControl = new FormControl('', [Validators.required]);

  constructor(private service: MachineService) {

    this.initConnection();

    connection.on("UpdateMachineList", (update: boolean) => {
      if (update)
        this.getMachines();
    });

    connection.on("ShowResponseCommand", (response: string) => {
      this.terminalResponse.push(response);

      this.automaticScroll();
    });

  }

  ngOnInit() {
    this.getMachines();
  }

  sendCommand() {

    let command = this.terminalCommand.value;

    if (command.length > 0) {
      connection
        .invoke('ExecuteTerminalCommand', command, this.selectedMachine.lastConnectionId)
        .catch(err => console.error(err));

      this.terminalCommand.reset();
    }
  }

  showTerminal(machine: MachineModel) {
    this.view = 'terminal';

    this.selectedMachine = machine;

    connection
      .invoke('OpenTerminal', machine.lastConnectionId)
      .catch(err => console.error(err));
  }

  back() {
    this.view = 'list';

    connection
      .invoke('CloseTerminal', this.selectedMachine.lastConnectionId)
      .catch(err => console.error(err));

    this.terminalResponse = [];
    this.terminalCommand.reset();
  }

  automaticScroll() {
    var terminal = document.getElementById("terminal");
    terminal.scrollTop = terminal.scrollHeight;
  }

  getMachines(): void {
    this.service
      .getMachines()
      .subscribe((data: MachineModel[]) => this.machines = data);
  }

  initConnection(): void {
    connection.start()
      .then(() => console.log('Signal Conectado'))
      .catch(err => {
        console.log(err);
      });
  }

  convertMbToGb(value: number): string {
    return Math.abs(value / 1024).toFixed(1);
  }

}
