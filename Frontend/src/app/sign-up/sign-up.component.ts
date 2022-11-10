import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ConfirmPasswordValidator } from '../_helpers/confirm-password.validator';
import { CustomerService } from '../_services/customer.service';
import { DirectionService } from '../_services/direction.service';
import { CustomerAuthenticationService } from '../_auth/customer-authentication.service';
import { SharedService } from '../_services/shared_service/shared.service';
import { ActivatedRoute, Router } from '@angular/router';
declare var $: any;

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css'],
})
export class SignUpComponent implements OnInit {
  registerForm: FormGroup;
  submitted: boolean = false;
  fieldTextType: boolean;
  errorMessage: any;
  successMessage: string;
  _date = new Date()
  new_date: any
  max_date: string;
  min_date: any


  constructor(
    private fb: FormBuilder,
    private router: Router,
    private sharedService: SharedService,
    private customerService: CustomerService,
    private directionService: DirectionService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {

    let year = this._date.getFullYear()
    let month = (1 + this._date.getMonth()).toString().padStart(2, '0')
    let day = this._date.getDate().toString().padStart(2, '0')

    this.new_date = `${year}-${month}-${day}`
    
    let _min_year = (this._date.getFullYear() - 18)
    this.min_date = `${_min_year}-${month}-${day}`

    this.registerForm = this.fb.group(
      {
        fname: [null, Validators.required],
        lname: [null, Validators.required],
        lname2: [null, Validators.required],
        identification: [null, Validators.required],
        username: [null, Validators.required],
        phone: [null],
        dob: [null],
        password: [null, Validators.required],
        confirmPassword: ['', Validators.required],

        province: ['', Validators.required],
        canton: ['', Validators.required],
        district: ['', Validators.required],
      },
      {
        validator: ConfirmPasswordValidator('password', 'confirmPassword'),
      }
    );
  }

  //get form controls
  get f() {
    return this.registerForm.controls;
  }

  sendUserData(message: string) {
    this.sharedService.nextCustomerMessage(message);
  }

  onSubmit() {
    this.submitted = true;

    let direction = {
      provincia: this.registerForm.value.province,
      canton: this.registerForm.value.canton,
      distrito: this.registerForm.value.district,
    }

    this.directionService.postDirection(direction).subscribe(
      id=>{
        let customer = {
          nombre: this.registerForm.value.fname,
          apellido1: this.registerForm.value.lname,
          apellido2: this.registerForm.value.lname2,
          cedula: this.registerForm.value.identification,
          usuario: this.registerForm.value.username,
          telefono: this.registerForm.value.phone,
          fecha_nac: this.registerForm.value.dob,
          contrasena: this.registerForm.value.password,
          id_direccion: id
        };
        this.customerService.postCustomer(customer).subscribe(
          res => {
            this.submitted = false;
          }
        )
      }
    )

  }

  toggleFieldTextType() {
    this.fieldTextType = !this.fieldTextType;
  }
}
