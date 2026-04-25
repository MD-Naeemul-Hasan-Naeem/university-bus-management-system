import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Bus {
  id?: number;
  busNumber?: string;
  capacity?: number;
  driverId?: number;
  status?: string;
  createdAt?: Date;
  updatedAt?: Date;
}

@Injectable({
  providedIn: 'root'
})
export class BusService {

  private baseUrl = 'https://localhost:7242/api/bus';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Bus[]> {
    return this.http.get<Bus[]>(`${this.baseUrl}/all`);
  }

  getById(id: number): Observable<Bus> {
    return this.http.get<Bus>(`${this.baseUrl}/${id}`);
  }

  create(bus: Bus): Observable<any> {
    return this.http.post(`${this.baseUrl}/create`, bus);
  }

  update(bus: Bus): Observable<any> {
    return this.http.put(`${this.baseUrl}/update`, bus);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/delete/${id}`);
  }

  changeStatus(id: number, status: string): Observable<any> {
    return this.http.patch(`${this.baseUrl}/status?id=${id}&status=${status}`, {});
  }
}