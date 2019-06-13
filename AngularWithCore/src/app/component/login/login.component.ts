import { Component, OnInit } from '@angular/core';
import { Login } from '../../models/Login';
import { LoginService } from '../../services/login.service'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  login:Login;

  constructor(private loginService: LoginService) { }

  ngOnInit() {
    this.login = {
      matricNo:  '',
      password:  ''
    }
  }

  onSubmit() {
    this.loginService.login(this.login).subscribe(login => {
      console.log(login);
    });
  }

}
