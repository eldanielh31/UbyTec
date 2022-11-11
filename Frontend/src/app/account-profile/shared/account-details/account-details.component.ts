import { Component, OnInit } from '@angular/core';
import { CustomerAuthenticationService } from 'src/app/_auth/customer-authentication.service';
import { CustomerService } from 'src/app/_services/customer.service';
import jwt_decode from 'jwt-decode';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-account-details',
  templateUrl: './account-details.component.html',
  styleUrls: ['./account-details.component.css'],
})
export class AccountDetailsComponent implements OnInit {
  pageTitle = 'Customer account overview | Maungano Food Express';
  customerProfileForm: FormGroup;

  userData: any;
  customerProfile: any;
  fname: any;
  lname: any;
  lname2: any;
  username: any;
  primaryPhone: any;
  cedula: any;
  password: any;
  id_direction: any;

  updating: boolean = false
  errorMsg: any;
  dob: any;

  constructor(
    private title: Title,
    private customerService: CustomerService,
    private fb: FormBuilder,
    private toast: ToastrService,
  ) { }

  ngOnInit(): void {
    this.title.setTitle(this.pageTitle);

    this.customerProfileForm = this.fb.group({
      fname: [null],
      lname: [null],
      lname2: [null],
      username: [null],
      primaryPhone: [null],
      dob: [null],
      password: [null],

      province: [null],
      canton: [null],
      district: [null],
    });


    //fetch all customer details
    this.customerProfile = JSON.parse(localStorage.getItem('currentUser'));
    this.cedula = this.customerProfile.cedula;

    this.customerService.getCustomerById(this.cedula).subscribe(
      data=>{
        let currentUser = data[0]
        localStorage.setItem('currentUser', JSON.stringify(currentUser))
      }
    )
    this.customerProfile = JSON.parse(localStorage.getItem('currentUser'));

    this.fname = this.customerProfile.nombre;
    this.lname = this.customerProfile.apellido1;
    this.lname2 = this.customerProfile.apellido2;
    this.username = this.customerProfile.usuario;
    this.primaryPhone = this.customerProfile.telefono;
    this.dob = this.customerProfile.fecha_nac;
    this.password = this.customerProfile.contrasena;
    this.id_direction = this.customerProfile.id_direccion;

    //format date before patching it to date  input element
    const date_of_birth = function formatDate(_date) {
      const d = new Date(_date);
      let month = '' + (d.getMonth() + 1);
      let day = '' + d.getDate();
      const year = d.getFullYear();
      if (month.length < 2) month = '0' + month;
      if (day.length < 2) day = '0' + day;
      return [year, month, day].join('-');
    }

    this.customerProfileForm.patchValue({
      fname: this.fname,
      lname: this.lname,
      lname2: this.lname2,
      username: this.username,
      password: this.password,
      primaryPhone: this.primaryPhone,
      dob: date_of_birth(this.dob)
    });
  }

  submit() {
    this.updating = true
    var fname = this.customerProfileForm.value.fname
    var lname = this.customerProfileForm.value.lname
    var lname2 = this.customerProfileForm.value.lname2
    var username = this.customerProfileForm.value.username
    var password = this.customerProfileForm.value.password
    var primaryPhone = this.customerProfileForm.value.primaryPhone
    var dob = this.customerProfileForm.value.dob

    const customer = {
      cedula: this.cedula,
      nombre: fname,
      apellido1: lname,
      apellido2: lname2,
      usuario: username,
      contrasena: password,
      telefono: primaryPhone,
      fecha_nac: dob,
      id_direccion: this.id_direction
    }

    this.customerService.updateCustomer(customer)
      .subscribe(data => {
        this.toast.success(
          `Customer information details updated`,
          'Information updated',
          {
            timeOut: 3600,
            progressBar: true,
            progressAnimation: 'increasing',
            positionClass: 'toast-top-right',
          }
        );
        this.updating = false
        this.ngOnInit()
      }, err => {
        this.errorMsg = err
      })

  }
}
