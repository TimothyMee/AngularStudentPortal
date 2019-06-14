import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Login } from '../models/Login';
import { Observable } from 'rxjs';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
  }

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  loginUrl: string = 'http://localhost:53136/api/login';
  

  constructor(private http:HttpClient) { }

  login(login: Login): Observable<any> {
    return this.http.post<any>(this.loginUrl, login, httpOptions);
  }
}
