import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, ParamMap } from "@angular/router";
import { ProductsService } from "../_services/products.service";
import { ProductModelServer } from "../_models/products";
import { map } from "rxjs/operators";
import { CartService } from "../_services/cart.service";
import { FeedbackService } from "../_services/feedback.service";
import { CustomerService } from '../_services/customer.service';

declare let $: any;

@Component({
  selector: 'app-product-page',
  templateUrl: './product-page.component.html',
  styleUrls: ['./product-page.component.css']
})

export class ProductPageComponent implements OnInit {
  id: Number;
  product: any;
  feedback: any[] = [];
  thumbimages: any[] = [];
  currentUser: any;


  @ViewChild('quantity') quantityInput;

  constructor(private route: ActivatedRoute,
    private productService: ProductsService,
    private cartService: CartService,
    private feedbackService: FeedbackService,
    private customerService: CustomerService) {


  }

  ngOnInit(): void {

    this.route.paramMap.pipe(
      map((param: ParamMap) => {
        // @ts-ignore
        return param.params.id;
      })
    ).subscribe(prodId => {
      this.id = prodId;
      //console.log("this id is", this.id)
      this.productService.getSingleProduct(this.id).subscribe(prod => {
        prod = prod[0];
        this.product = prod;
        this.thumbimages = [prod.foto]

      });

      this.feedbackService.getFeedbackByIdProducto(this.id).subscribe(fbs=>{
        this.feedback = fbs;
      })

    });
    
    this.currentUser = JSON.parse(localStorage.getItem("currentUser"));

  }

  sendReview(review){
    console.log(review)
  }

  addToCart(id: Number) {
    this.cartService.AddProductToCart(id, this.quantityInput.nativeElement.value);
  }

  Increase() {
    let value = parseInt(this.quantityInput.nativeElement.value);
    if (this.product.quantity >= 1) {
      value++;

      if (value > this.product.quantity) {
        // @ts-ignore
        value = this.product.quantity;
      }
    } else {
      return;
    }

    this.quantityInput.nativeElement.value = value.toString();
  }

  Decrease() {
    let value = parseInt(this.quantityInput.nativeElement.value);
    if (this.product.quantity > 0) {
      value--;

      if (value <= 0) {
        // @ts-ignore
        value = 0;
      }
    } else {
      return;
    }
    this.quantityInput.nativeElement.value = value.toString();


  }

}
