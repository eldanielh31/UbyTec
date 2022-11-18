import { Component, OnInit } from '@angular/core';
import { CustomerAuthenticationService } from '../../../_auth/customer-authentication.service';
import { CustomerService } from '../../../_services/customer.service';
import jwt_decode from 'jwt-decode';

import { OrderService } from '../../../_services/order.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-my-orders',
  templateUrl: './my-orders.component.html',
  styleUrls: ['./my-orders.component.css'],
})
export class MyOrdersComponent implements OnInit {
  ordersOpen: any;
  ordersClosed: any;
  p: number = 1;
  count: any;
  currentCedula: any;

  constructor(
    private customerService: CustomerService,
    public custAuthService: CustomerAuthenticationService,
    private orderService: OrderService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit() {
    //fetch user 
    this.currentCedula = JSON.parse(localStorage.getItem("currentUser")).cedula

    //fetch orders by customer id
    this.spinner.show();
    this.orderService.getAllOrdersClient().subscribe(
      (data) => {
        data = data.filter(obj => obj.cedula_cliente === this.currentCedula)
        //filter open orders
        this.ordersOpen = data.filter(obj => obj.entregado === false);
        //filter closed orders
        this.ordersClosed = data.filter(obj => obj.entregado === true);
        this.spinner.hide();
      },
      (err) => { }
    );
  }

  cancel(id: number) {
    if (window.confirm(`Are you sure you want to cancel order with reference ${id}?`)) {
      this.spinner.show();
      //call cancel order api
      this.orderService.cancelOrder(id)
        .subscribe(data => {
          console.log("Distinct orders after refresh", data)
          this.ngOnInit()
          this.spinner.hide();
        })
    } else {
      return;
    }
  }

  refresh() {
    this.ngOnInit()
  }
}
