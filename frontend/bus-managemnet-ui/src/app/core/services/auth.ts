// src/app/core/services/auth.service.ts
import { Injectable } from '@angular/core';
import { ApiService } from './api';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private api: ApiService) {}

  login(data: any): Observable<any> {
    return this.api.post('UsersInfo/login', data);
  }

  register(data: any): Observable<any> {
    return this.api.post('UsersInfo/save', data);
  }

  saveToken(token: string) {
    localStorage.setItem('token', token);
  }

  getToken() {
    return localStorage.getItem('token');
  }

  getRole() {
    return localStorage.getItem('role');
  }

  logout() {
    localStorage.clear();
  }
}
