// src/app/core/services/auth.service.ts

import { Injectable } from '@angular/core';
import { ApiService } from './api';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private api: ApiService) {}

  login(data: any): Observable<any> {
    return this.api.post('UsersInfo/login', data).pipe(
      tap((res: any) => {
        if (res?.token) {
          this.saveToken(res.token);
          this.saveRole(res.role);
        }
      })
    );
  }

  register(data: any) {
  return this.api.post('UsersInfo/register', data);
}

  private saveToken(token: string) {
    localStorage.setItem('token', token);
  }

  private saveRole(role: string) {
    localStorage.setItem('role', role);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getRole(): string | null {
    return localStorage.getItem('role');
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  logout() {
    localStorage.clear();
  }
}