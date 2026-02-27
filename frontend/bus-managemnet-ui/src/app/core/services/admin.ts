import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { DashboardStats } from '../models/dashboard-stats.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  private apiUrl = environment.baseUrl + 'Admin/'; // <-- only once

  constructor(private http: HttpClient) {}

  // POST create user
  createUser(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}create-user`, data);
  }

  // GET dashboard stats
  getDashboardStats(): Observable<DashboardStats> {
    return this.http.get<DashboardStats>(`${this.apiUrl}dashboard`);
  }
}