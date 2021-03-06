import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  profileUrl: string = 'http://localhost:53136/api/dashboardprofile';
  constructor(private http: HttpClient) {
  }

  getProfile(matricNo: string): Observable<any> {
    return this.http.get<any>(`${this.profileUrl}/${matricNo}`);
  }
}
