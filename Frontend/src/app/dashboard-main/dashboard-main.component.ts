import { Component, OnInit } from '@angular/core';
import { OrderService } from '../_services/order.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-dashboard-main',
  templateUrl: './dashboard-main.component.html',
  styleUrls: ['./dashboard-main.component.css'],
})
export class DashboardMainComponent implements OnInit {
  count: any;
  ordersOpen: any;

  public searchText: string;
  public searchState: number;

  p: number = 1;

  constructor(
    private orderService: OrderService,
    private spinner: NgxSpinnerService
  ) {}

  ngOnInit(): void {
    this.spinner.show();

    this.orderService.getAllDistinctOrders().subscribe(
      (data) => {
        
        this.ordersOpen = data.filter((obj) => obj.entregado === false || obj.entregado === true);
        this.count = data.length;
        console.log('Distinct open orders', this.ordersOpen);
        /** spinner ends after api fetch is complete */
        this.spinner.hide();
      },
      (err) => {
        console.log(err);
      }
    );
  }

  cancel(id:number){
    this.ordersOpen = this.ordersOpen.filter((order)=> order.id !== id )

    this.orderService.deleteOrder(id).subscribe(
      (data) => {
        console.log(data)
      },
      (err) => {
        console.log(err);
      }
    );
  }

  refresh() {
    this.ngOnInit();
  }
}
