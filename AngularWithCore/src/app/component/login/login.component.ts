import { Component, OnInit } from '@angular/core';
import { Login } from '../../models/Login';
import { LoginService } from '../../services/login.service'
import { Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  login: FormGroup;
  loginInfo: Login;
  constructor(private loginService: LoginService, private router: Router) { }

  ngOnInit() {
    this.login = new FormGroup({
      'matricNo': new FormControl(null, [Validators.required]),
      'password': new FormControl(null, [Validators.required])
    })
  }

  onSubmit() {
    this.populateLoginInfo();
    this.loginService.login(this.loginInfo).subscribe(login => {
      if (login.status === 'Success') {
        this.router.navigate(['/dashboard']);
      }
      console.log(login);
    });
  }

  populateLoginInfo() {
    this.loginInfo = {
      matricNo: this.login.get('matricNo').value,
      password: this.login.get('password').value
    }
  }
}
