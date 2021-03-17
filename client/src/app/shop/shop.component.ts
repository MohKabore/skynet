import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IProduct } from '../shared/models/product';
import { IProductType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  @ViewChild('search', {static:false}) searchTerm : ElementRef;
  products: IProduct[];
  types: IProductType[];
  brands: IBrand[];
 shopParams  =new ShopParams();
 totalCount : number;
  sortOptions = [
    {name :'Alphabetic', value : 'name'},
    {name :'Price: low to high', value : 'priceAsc'},
    {name :'Price: High to low', value : 'priceDesc'}


  ];
  constructor(private shopService: ShopService) { }

  ngOnInit() {
  this.getProducts();
  this.getBrands();
  this.getTypes();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe(response => {
      this.products = response.data;
      this.shopParams.pageNumber = response.pageIndex;
      this.shopParams.pageSize = response.pageSize;
      this.totalCount = response.count;
    }, error => {
      console.log(error);
    });
  }

  getBrands() {
    this.shopService.getBrands().subscribe(response => {
      this.brands = [{id:0, name:'All'},...response];
    }, error => {
      console.log(error);
    });
  }

  getTypes() {
    this.shopService.getTypes().subscribe(response => {
      this.types = [{id:0, name:'All'},...response];

    }, error => {
      console.log(error);
    });
  }

  onBrandSelected(brindId: number) {
    this.shopParams.brandId = brindId;
    this.shopParams.pageNumber =1;
    this.getProducts();
  }

  onTypeSelected(tpyeId: number) {
    this.shopParams.typeId = tpyeId;
    this.shopParams.pageNumber =1;
    this.getProducts();
  }

  onSortSelected(sort: string) {
    this.shopParams.sort = sort;
    this.getProducts();
  }
  onPageChanged(event: number) {
  if(this.shopParams.pageNumber!==event) {
    this.shopParams.pageNumber = event;
    this.getProducts();
  }
  }

  onSearch() {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber =1;
    this.getProducts();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
