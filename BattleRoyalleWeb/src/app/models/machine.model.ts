import { DiskModel } from './disk.model';

export class MachineModel {
    id: string;
    name: string;
    ipAddress: string;
    osVersion: string;
    creationDate: Date;
    hasAntivirus: boolean;
    antivirusName: string;
    lastConnectionId: string;
    disks: DiskModel[];
}