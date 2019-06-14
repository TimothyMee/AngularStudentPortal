import { Component, OnInit, Output, EventEmitter } from '@angular/core';
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
  show: boolean;
  constructor(private loginService: LoginService, private router: Router) { }

  ngOnInit() {
    this.show = false;
    this.login = new FormGroup({
      'matricNo': new FormControl(null, [Validators.required]),
      'password': new FormControl(null, [Validators.required])
    })
  }
  onSubmit() {
    this.show = true;
    this.populateLoginInfo();
    this.loginService.login(this.loginInfo).subscribe(login => {
      if (login.status === 'Success') {
        localStorage.setItem("mat", this.loginInfo.matricNo);
        this.router.navigate(['/dashboard']);
      }
      console.log(login);
    });
  }

  //@Output() passingmatricno = new EventEmitter<any>();

  //getMatricNo() {
  //  debugger;
  //  this.populateLoginInfo();
  //  this.passingmatricno.emit(this.loginInfo.matricNo);
  //  this.show = true;
  //  //this.populateLoginInfo();
  //  this.loginService.login(this.loginInfo).subscribe(login => {
  //    if (login.status === 'Success') {
  //      localStorage.setItem("mat", this.loginInfo.matricNo);
  //      this.router.navigate(['/dashboard']);
  //    }
  //    console.log(login);
  //  });
  //}

  populateLoginInfo() {
    this.loginInfo = {
      matricNo: this.login.get('matricNo').value,
      password: this.login.get('password').value
    }
  }
}
