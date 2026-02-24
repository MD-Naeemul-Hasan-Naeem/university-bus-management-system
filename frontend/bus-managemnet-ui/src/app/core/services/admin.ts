import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  private apiUrl = environment.baseUrl;

  constructor(private http: HttpClient) {}

  createUser(data: any) {
    return this.http.post(`${this.apiUrl}Admin/create-user`, data);
  }
}