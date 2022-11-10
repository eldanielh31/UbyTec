import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import jwt_decode from 'jwt-decode';
import { CustomerAuthenticationService } from '../_auth/customer-authentication.service';
import { SharedService } from '../_services/shared_service/shared.service';
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { CustomerService } from '../_services/customer.service';
import { CustomersModelServer } from '../_models/customers';

export interface DialogData {
  message: string;
  dialogState: number
}
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  pageTitle = 'Login | Maungano Food Express';
  loginForm: FormGroup;
  fieldTextType: boolean;
  submitted: boolean = false;
  successMsg: string;
  errorMsg: any;
  loading = false;
  public loadingMsg = 'Authenticating...Please wait';
  _userData: any;
  _state: number;
  _visible: boolean;

  constructor(
    private title: Title,
    private sharedService: SharedService,
    private customerService: CustomerService, 
    private router: Router,
    private fb: FormBuilder,
  ) {

  }

  ngOnInit(): void {
    this.title.setTitle(this.pageTitle);

    this.loginForm = this.fb.group({
      username: [null, Validators.required],
      password: [null, Validators.required],
    });
  }

  //get form controls
  get f() {
    return this.loginForm.controls;
  }

  //Toggle show password
  toggleFieldTextType() {
    this.fieldTextType = !this.fieldTextType;
  }

  //Share user data via shared service
  sendUserData(message: string) {
    this.sharedService.nextCustomerMessage(message);
  }


  //child page toggler
  TogglePage(pageValue: string) {

    switch (pageValue) {
      case 'register':
        this._visible = true
        break;
      case 'login':
        this._visible = false
        break;
    }
  }

  //login
  login() {
    this.errorMsg = '';
    this.submitted = true;

    if (this.loginForm.invalid) {
      return;
    }
    this.loading = true;
    this.customerService.getCustomerByUsername(this.loginForm.value.username).subscribe(
      (data) => {
        let customer = data[0];

        if(customer && (customer['contrasena'] === this.loginForm.value.password)){
          this.successMsg = 'Successful Authentication';
          this.loading = false;
          this.submitted = false;
          localStorage.setItem('currentUser', JSON.stringify(customer));
          this.router.navigateByUrl('/home');
        }else{
          this.successMsg = 'Error Authentication';
          this.loading = false;
          this.submitted = false;
          this.errorMsg = 'Username or password is incorrect';
        } 
      },
      (err) => {
        this.errorMsg = err.error.reason;
        this.loading = false;
        this.submitted = false;
      }
    );

  }

}
