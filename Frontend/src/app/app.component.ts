import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { AuthenticationService } from './_auth/authentication.service';
import jwt_decode from 'jwt-decode';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  pageTitle = 'UbyTEC';

  sideBarOpen = true

  _route_url = "/home"
  fname: any;
  lname: any;

  constructor(private router: Router, public auth: AuthenticationService, private title: Title) { }

  ngOnInit() {
    this.title.setTitle(this.pageTitle)

    let customerData = localStorage.getItem('currentUser') ? JSON.parse(localStorage.getItem('currentUser')) : null

    this.fname = customerData ? customerData.nombre : null
    this.lname = customerData ? customerData.apellido1 : null
  }

  hasRoute(route: string) {
    return this.router.url.includes(route)
  }
}
