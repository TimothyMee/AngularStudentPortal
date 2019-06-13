import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import{ Login } from '../../models/Login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  login:Login;

  constructor() { }

  ngOnInit() {
    this.login = {
      matricNo: '990990',
      password: 'femi'      
    }
  }

}
