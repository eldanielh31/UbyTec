import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NgxSpinnerService } from 'ngx-spinner';
import { OrderService } from '../_services/order.service';

@Component({
  selector: 'app-admin-order-list',
  templateUrl: './admin-order-list.component.html',
  styleUrls: ['./admin-order-list.component.css'],
})
export class AdminOrderListComponent implements OnInit {
  pageTitle = 'Orders list | UbyTEC';
  ordersOpen: any;

  public searchText: string;
  public searchState: number;
  p: number = 1;
  count: any

  constructor(
    private title: Title,
    private orderService: OrderService,
    private spinner: NgxSpinnerService
  ) {}

  ngOnInit(): void {
    this.title.setTitle(this.pageTitle);
    this.spinner.show();
    this.orderService.getAllOrdersClient().subscribe((data) => {
      console.log(data);
      this.count = data.count;
      this.ordersOpen = data;
      this.spinner.hide();
    },
    err=>{});
  }

  refresh() {
    this.ngOnInit();
  }

  cancel(id: number){
    this.ordersOpen = this.ordersOpen.filter((ord) => ord.id !== id);
    this.orderService.deleteOrder(id).subscribe(
      (data) => {
        console.log(data);
      },
      (err) => {
        console.log(err);
      }
    );
  }
}
