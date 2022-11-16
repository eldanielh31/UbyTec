import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NgxSpinnerService } from 'ngx-spinner';
import { ProductsService } from '../_services/products.service';

@Component({
  selector: 'app-admin-product-list',
  templateUrl: './admin-product-list.component.html',
  styleUrls: ['./admin-product-list.component.css'],
})
export class AdminProductListComponent implements OnInit {
  pageTitle = 'Menu list | UbyTEC';
  menuItems: any;

  constructor(
    private title: Title,
    private menuService: ProductsService,
    private spinner: NgxSpinnerService
  ) {}

  ngOnInit(): void {
    this.title.setTitle(this.pageTitle);
    this.spinner.show();
    //fetch menu Items
    this.menuService.getProducts().subscribe((data) => { 
      this.menuItems = data;
      console.log(`Menu`, this.menuItems);
      this.spinner.hide();
    }, err=>{});
  }

  refresh() {
    this.ngOnInit();
  }

  cancel(id:number){
    this.menuItems = this.menuItems.filter((prod) => prod.id !== id);
    this.menuService.deleteProduct(id).subscribe(
      (data) => {
        console.log(data);
      },
      (err) => {
        console.log(err);
      }
    );
  }
}
