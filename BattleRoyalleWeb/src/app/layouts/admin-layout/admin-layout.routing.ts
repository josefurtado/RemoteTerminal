import { Routes } from "@angular/router";
import { MachineListComponent } from 'src/app/pages/machines/machine-list.component';


export const AdminLayoutRoutes: Routes = [
  { path: "machine-list", component: MachineListComponent },
];
