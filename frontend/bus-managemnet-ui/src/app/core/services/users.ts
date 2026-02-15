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
}
