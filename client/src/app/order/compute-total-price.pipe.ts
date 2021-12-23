import { Pipe, PipeTransform } from '@angular/core';
import { CurrentOrder } from './order.model';

@Pipe({
  name: 'computeTotalPrice'
})
export class ComputeTotalPricePipe implements PipeTransform {

  transform(order: CurrentOrder | null ): CurrentOrder | null {
    if (!order) {
      return null;
    }

    const totalPrice = order.products.reduce((acc, crtProd) => acc + (+crtProd.price * +crtProd.quantity), 0);
    order.totalPrice = totalPrice;

    return order;
  }
}
