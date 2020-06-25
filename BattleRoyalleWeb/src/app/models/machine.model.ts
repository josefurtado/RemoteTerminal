import { DiskModel } from './disk.model';

export class MachineModel {
    id: string;
    name: string;
    ipAddress: string;
    osVersion: string;
    creationDate: Date;
    lastConnectionId: string;
    disks: DiskModel[];
}