import { HttpClient } from '@angular/common/http';
import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { IPagination } from './models/pagination';
import { IProduct } from './models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {


  constructor(private http: HttpClient) { }
  title = 'client';
  products : IProduct[];
  ngOnInit(): void {
    this.http.get("https://localhost:5001/api/products").subscribe((response: IPagination) => {
     this.products = response.data;
     console.log(this.products)

    }, error => {
      console.log(error);
    });
  }
}
