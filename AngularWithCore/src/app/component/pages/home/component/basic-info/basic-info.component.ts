import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-basic-info',
  templateUrl: './basic-info.component.html',
  styleUrls: ['./basic-info.component.css']
})
export class BasicInfoComponent implements OnInit {
  programme: string;
  level: string;
  constructor() { }

  ngOnInit() {
    this.level = '4';
    this.programme = 'Computer Science';
  }

}
