import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NgxSpinnerService } from 'ngx-spinner';
import { CustomerService } from '../_services/customer.service';
import { OrderService } from '../_services/order.service';
import { InvoiceService } from '../_services/invoice.service';

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
    private spinner: NgxSpinnerService,
    private orderService: OrderService,
    private invoiceService: InvoiceService,
    ) { }

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

  reportPDF(cedula: number) {
    this.orderService.getConsolidadoVentasByCedula(cedula).subscribe(
      ven => {
        ven = ven[0]
        this.customerService.getCustomerById(cedula).subscribe(
          rest => {
            rest = rest[0]
            this.invoiceService.invoice.customerName = `${rest.nombre} ${rest.apellido1} ${rest.apellido2}`
            this.invoiceService.invoice.address = `${rest.provincia} ${rest.canton} ${rest.distrito}`
            this.invoiceService.invoice.contactNo = rest.telefono
            this.invoiceService.invoice.email = ""
            this.invoiceService.invoice.products = [
              {
                identification: ven.cedula,
                total: ven.total_pagado,
                qty: ven.cantidad_compras
              }
            ]

            console.log(this.invoiceService.invoice)

            this.invoiceService.generatePDF()
          }
        )

      }
    )
  }

}
