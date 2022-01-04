import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { filter, map, Observable, of, tap } from 'rxjs';
import { OrderService } from 'src/app/order/order.service';
import { WritableProduct, Product } from '../product.model';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-single-product',
  templateUrl: './single-product.component.html',
  styleUrls: ['./single-product.component.scss']
})
export class SingleProductComponent implements OnInit {
  updatedProductId = -1;
  form!: FormGroup;
  isCurrentProductReady$!: Observable<boolean>;

  get isAddMode () {
    return this.updatedProductId === -1;
  }

  get title () {
    return this.isAddMode
      ? 'Adding a new product'
      : `Updating the product with the ID ${this.updatedProductId}`;
  }

  constructor (
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private productService: ProductService,
    private router: Router,
    private orderService: OrderService,
  ) {
    this.initializeComponent();
  }

  ngOnInit(): void {
  }

  private initializeComponent () {
    this.form = this.fb.group({
      name: this.fb.control(''),
      price: this.fb.control(0),
      category: this.fb.control('0'),
    });

    const [urlSegment] = this.route.snapshot.url;
    if (/add/i.test(urlSegment.path)) {
      this.isCurrentProductReady$ = of(true);
      return;
    }

    this.updatedProductId = +this.route.snapshot.paramMap.get("id")!;
    this.isCurrentProductReady$ = this.productService.products$
      .pipe(
        map((products: Product[]) => products.find(p => +p.id === this.updatedProductId)),
        tap({ next: p => {
          if (!p) {
            throw new Error('The product could not be found!');
          }

          this.form.get('name')?.setValue(p.name);
          this.form.get('price')?.setValue(p.price);
          this.form.get('category')?.setValue(p.category);
        }}),
        map(p => !!p),
      );
  }

  onSubmit (formValues: WritableProduct) {
    if (this.isAddMode) {
      this.addProduct(formValues);
    } else {
      this.updatedProduct(formValues);
    }
  }

  private addProduct (formValues: WritableProduct) {
    this.productService.addProduct(formValues)
      .subscribe(() => (this.productService.markProductsAsDirty(), this.router.navigateByUrl('/products')));
  }

  private updatedProduct (updatedValues: WritableProduct) {
    return this.productService.updatedProduct(updatedValues, this.updatedProductId)
      .subscribe(() => (this.productService.markProductsAsDirty(), this.orderService.markCurrentOrderAsDirty(), this.router.navigateByUrl('/products')));
  }
}
