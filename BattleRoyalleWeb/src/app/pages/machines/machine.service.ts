import { Injectable } from "@angular/core";
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class MachineService {
    url: string;
    headers: HttpHeaders;

    constructor(private http: HttpClient) {
        this.headers = new HttpHeaders().set('Content-Type', 'application/json');
        this.url = 'http://localhost:50198/api';
    }

    getMachines(): Observable<any[]> {
        return this.http.get<any[]>(`${this.url}/machines`, { headers: this.headers });
    }
}