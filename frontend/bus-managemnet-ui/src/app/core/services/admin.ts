import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { DashboardStats } from '../models/dashboard-stats.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  private apiUrl = environment.baseUrl + 'Admin/';

  constructor(private http: HttpClient) {}

  createUser(data: any) {
    return this.http.post(`${this.apiUrl}Admin/create-user`, data);
  }
  getDashboardStats(): Observable<DashboardStats> {
    return this.http.get<DashboardStats>(`${this.apiUrl}dashboard`);
  }
}