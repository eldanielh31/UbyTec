import { Component, OnInit } from '@angular/core';
import { CustomerAuthenticationService } from '../_auth/customer-authentication.service';
import { CustomerService } from '../_services/customer.service';
import { SharedService } from '../_services/shared_service/shared.service';
import jwt_decode from 'jwt-decode';
import { CustomersModelServer } from '../_models/customers';
import { FormBuilder, FormGroup } from '@angular/forms';
import { OrderService } from '../_services/order.service';

@Component({
  selector: 'app-account-profile',
  templateUrl: './account-profile.component.html',
  styleUrls: ['./account-profile.component.css'],
})
export class AccountProfileComponent implements OnInit {

  userData: any;
  customerProfile: any;
  fname: any;
  lname: any;
  lname2: any;
  username: any;
  password: any;
  phone: any;

  add_new: boolean = false;
  orders: any;

  constructor(
   
    private customerService: CustomerService,
    public custAuthService: CustomerAuthenticationService,
    private orderService: OrderService
  ) {}

  ngOnInit(): void {

    //fetch all customer details
    this.customerProfile = JSON.parse(localStorage.getItem('currentUser'));
    this.fname = this.customerProfile.nombre;
    this.lname = this.customerProfile.apellido1;
    this.lname2 = this.customerProfile.apellido1;
    this.username = this.customerProfile.usuario;
    this.password = this.customerProfile.contrasena;
    this.phone = this.customerProfile.telefono;
  }

  submit(btn_value: string) {
    //console.log("This details form", this.detailsForm.value)
    switch (btn_value) {
      
      case 'address':
        console.log('This address form');
        this.add_new = false;
        break;
      default:
        console.log('No value has been selected');
    }
  }

  
}
