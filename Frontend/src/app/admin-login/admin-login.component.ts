import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../_auth/authentication.service';
import { AdminService } from '../_services/admin.service';

@Component({
  selector: 'app-admin-login',
  templateUrl: './admin-login.component.html',
  styleUrls: ['./admin-login.component.css'],
})
export class AdminLoginComponent implements OnInit {
  pageTitle = 'SYSTEM Admin | Log in';

  loginForm: FormGroup;

  submitted: boolean = false;
  successMsg: string;
  errorMsg: any;
  loading = false;
  public loadingMsg = 'Authenticating...Please wait';
  returnUrl: string

  constructor(
    private formbuilder: FormBuilder,
    private route: ActivatedRoute,
    private authService: AuthenticationService,
    private title: Title,
    private router: Router,
    private customerService: AdminService
  ) {}

  ngOnInit(): void {
    this.title.setTitle(this.pageTitle);
    //initialize form
    this.loginForm = this.formbuilder.group({
      username: ["", Validators.required],
      password: [null, Validators.required],
    });

    //get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/admin/dashboard';
    console.log("This url", this.returnUrl)
  }

  //get form controls
  get f() {
    return this.loginForm.controls;
  }

  login() {
    this.errorMsg = '';
    this.submitted = true;

    if (this.loginForm.invalid) {
      return;
    }
    this.loading = true;

    this.customerService.getAdminByUsername(this.loginForm.value.username).subscribe(
      (data) => {
        let customer = data[0];

        if (customer && (customer['contrasena'] === this.loginForm.value.password)) {
          this.successMsg = 'Successful Authentication';
          this.loading = false;
          this.submitted = false;
          localStorage.setItem('currentAdmin', JSON.stringify(customer));
          this.router.navigate([this.returnUrl]);
        } else {
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
