import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrdersRoutingModule } from './orders-routing.module';
import { SharedModule } from '../shared/shared.module';
import { OrdersComponent } from './orders.component';
import { OrderDetailedComponent } from './order-detailed/order-detailed.component';



@NgModule({
  declarations: [OrderDetailedComponent],
  imports: [
    CommonModule,
    SharedModule,
    OrdersRoutingModule
  ]
})
export class OrdersModule { }
