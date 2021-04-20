import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IOrder } from 'src/app/shared/models/order';
import { BreadcrumbService } from 'xng-breadcrumb';
import { OrdersService } from '../orders.service';

@Component({
  selector: 'app-order-detailed',
  templateUrl: './order-detailed.component.html',
  styleUrls: ['./order-detailed.component.scss']
})
export class OrderDetailedComponent implements OnInit {
order:  IOrder;
  constructor(private breadcrumbService: BreadcrumbService,private ordersService: OrdersService,
    private router: ActivatedRoute) {
    this.breadcrumbService.set('@orderDetailed','');
   }

  ngOnInit(): void {
    this.ordersService.getOrderDetailed(+this.router.snapshot.paramMap.get('id')).subscribe((order: IOrder)=>{
      this.order = order;
      this.breadcrumbService.set('@OrderDetailed', `Order# ${order.id} - ${order.status}`);

    }, error=>{
      console.log(error);
    })
  }

}
