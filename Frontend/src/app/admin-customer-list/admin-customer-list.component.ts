import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NgxSpinnerService } from 'ngx-spinner';
import { CustomerService } from '../_services/customer.service';

@Component({
  selector: 'app-admin-customer-list',
  templateUrl: './admin-customer-list.component.html',
  styleUrls: ['./admin-customer-list.component.css']
})
export class AdminCustomerListComponent implements OnInit {
  count: any;
  customers: any;
  errorMsg: any;

  constructor(
    private customerService: CustomerService,
    private title: Title,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.spinner.show()

    this.customerService.getCustomers()
    .subscribe(data=>{
      this.count = data.length
      this.customers = data
      this.spinner.hide()
    }, err =>{
      this.errorMsg = err
      this.spinner.hide()
    })
  }

  updateState(_status: number){
    console.log("Selected states", _status)
  }

  refresh(){
    this.ngOnInit()
  }

  cancel(cedula: number){
    this.customers = this.customers.filter((cus)=>cus.cedula !== cedula)

    this.customerService.deleteCustomer(cedula).subscribe(
      (data) => {
        console.log(data)
      },
      (err) => {
        console.log(err);
      }
    );
  }

}
