import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private source = new BehaviorSubject<string>("a12365478");
  matricno = this.source.asObservable();
  constructor() { }

  changeMatric(data: string) {
    this.source.next(data);
  }
}
