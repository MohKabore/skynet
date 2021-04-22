import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';
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
  types: IType[];
  brands: IBrand[];
 shopParams  :ShopParams;
 totalCount : number;
  sortOptions = [
    {name :'Alphabetic', value : 'name'},
    {name :'Price: low to high', value : 'priceAsc'},
    {name :'Price: High to low', value : 'priceDesc'}


  ];
  constructor(private shopService: ShopService) {
    this.shopParams = this.shopService.getShopParams();
   }

  ngOnInit() {
  this.getProducts(true);
  this.getBrands();
  this.getTypes();
  }

  getProducts(useCache=false) {
    this.shopService.getProducts(useCache).subscribe(response => {
      this.products = response.data;
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
    const params = this.shopService.getShopParams();
    params.brandId = brindId;
    params.pageNumber =1;
    this.shopService.setShopParams(params);
    this.getProducts();
  }

  onTypeSelected(tpyeId: number) {
    const params = this.shopService.getShopParams();
    params.typeId = tpyeId;
    params.pageNumber =1;
    this.shopService.setShopParams(params);
    this.getProducts();
  }

  onSortSelected(sort: string) {
    const params = this.shopService.getShopParams();
    this.shopParams.sort = sort;
    this.shopService.setShopParams(params);
    this.getProducts();
  }

  onPageChanged(event: number) {
    const params = this.shopService.getShopParams();
  if(params.pageNumber!==event) {
    params.pageNumber = event;
    this.shopService.setShopParams(params);
    this.getProducts(true);
  }
  }

  onSearch() {
    const params = this.shopService.getShopParams();
    params.search = this.searchTerm.nativeElement.value;
    params.pageNumber =1;
    this.shopService.setShopParams(params);
    this.getProducts();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.shopParams= new ShopParams();
    this.shopService.setShopParams(this.shopParams);
    this.getProducts();
  }
}
