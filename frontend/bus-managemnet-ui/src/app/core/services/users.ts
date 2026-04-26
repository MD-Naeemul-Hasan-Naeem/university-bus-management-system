// src/app/core/services/users.service.ts
import { Injectable } from '@angular/core';
import { ApiService } from './api';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  constructor(private api: ApiService) {}

  getAll(): Observable<any> {
    return this.api.get('Users/GetAllUsers');
  }

  getById(id: number): Observable<any> {
    return this.api.get(`Users/GetUsersById/${id}`);
  }

  save(data: any): Observable<any> {
    return this.api.post('Users/SaveUsers', data);
  }

  delete(id: number): Observable<any> {
    return this.api.delete(`Users/DeleteUsers/${id}`);
  }
   // =========================
  // NEW USERS INFO API (JWT + Admin system)
  // Controller: UsersInfoController
  // =========================

  // 🔹 Get all users (Admin only)
  getAllUsersInfo(): Observable<any> {
    return this.api.get('UsersInfo');
  }

  // 🔹 Get user by id
  getUserInfoById(id: number): Observable<any> {
    return this.api.get(`UsersInfo/${id}`);
  }

  // 🔹 Delete (soft delete)
  deleteUserInfo(id: number): Observable<any> {
    return this.api.delete(`UsersInfo/${id}`);
  }

  // 🔹 Update role
  updateRole(id: number, role: string): Observable<any> {
    return this.api.put(`UsersInfo/update-role/${id}`, role);
  }

  // 🔹 Lock / Unlock user
  lockUnlockUser(id: number, isLocked: boolean): Observable<any> {
    return this.api.put(`UsersInfo/lock-unlock/${id}`, isLocked);
  }

  // 🔹 Reset password
  resetPassword(id: number, newPassword: string): Observable<any> {
    return this.api.put(`UsersInfo/reset-password/${id}`, newPassword);
  }

  // 🔹 Login (JWT)
  login(data: any): Observable<any> {
    return this.api.post('UsersInfo/login', data);
  }

  // 🔹 Register
  register(data: any): Observable<any> {
    return this.api.post('UsersInfo/register', data);
  }

  createUser(data: any) {
  return this.api.post('UsersInfo/register', data);
}
}
