import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NgxSpinnerService } from 'ngx-spinner';
import { RestaurantsService } from '../_services/restaurants.service';

@Component({
  selector: 'app-admin-restaurant-list',
  templateUrl: './admin-restaurant-list.component.html',
  styleUrls: ['./admin-restaurant-list.component.css'],
})
export class AdminRestaurantListComponent implements OnInit {
  pageTitle = 'Restaurants list | UbyTEC';
  restaurants: any;
  searchText: string;
  searchState: string;

  p: number = 1;
  count: any

  constructor(
    private title: Title,
    private restaurantService: RestaurantsService,
    private spinner: NgxSpinnerService
  ) {}

  ngOnInit(): void {
    this.spinner.show();
    //fetch restaurants
    this.restaurantService.getAllSuppliers().subscribe((data) => {
      this.restaurants = data;
      console.log('restaurantes', data)
      this.spinner.hide();
    });
  }

  activationUpdate(id: number, status: number) {
    const updateInfo = {
      cedula: id,
      status: status,
    };
    console.log(status);
    console.log('Supplier id', id);
    this.restaurantService.updateStatus(updateInfo).subscribe((data) => {
      console.log(data);
      this.ngOnInit();
    });
  }


  delete(cedula: number){
    this.restaurants = this.restaurants.filter((res) => res.cedula !== cedula);
    this.restaurantService.deleteRestaurant(cedula).subscribe(
      (data) => {
        console.log(data);
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
