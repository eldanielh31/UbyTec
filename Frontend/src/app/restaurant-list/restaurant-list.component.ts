import { Component, OnInit } from '@angular/core';
import { RestaurantsService } from '../_services/restaurants.service';

@Component({
  selector: 'app-restaurant-list',
  templateUrl: './restaurant-list.component.html',
  styleUrls: ['./restaurant-list.component.css']
})
export class RestaurantListComponent implements OnInit {
  restaurants: [] =[];
  
  serverMsg: string;
  errorMsg: any;

  constructor(private restaurantService: RestaurantsService) { }

  ngOnInit(): void {

    //fetch restaurants
    this.restaurants = JSON.parse(localStorage.getItem('restaurants'))
  }

}
