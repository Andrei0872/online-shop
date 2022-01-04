import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AddProduct } from '../product.model';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-single-product',
  templateUrl: './single-product.component.html',
  styleUrls: ['./single-product.component.scss']
})
export class SingleProductComponent implements OnInit {
  updatedProductId = -1;
  form: FormGroup;

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
  ) {
    this.initializeComponent();

    this.form = this.fb.group({
      name: this.fb.control(''),
      price: this.fb.control(0),
      category: this.fb.control('0'),
    });
  }

  ngOnInit(): void {
  }

  private initializeComponent () {
    const [urlSegment] = this.route.snapshot.url;
    if (/add/i.test(urlSegment.path)) {
      return;
    }

    this.updatedProductId = +this.route.snapshot.paramMap.get("id")!;
  }

  onSubmit (formValues: AddProduct) {
    this.productService.addProduct(formValues)
      .subscribe(() => (this.productService.markProductsAsDirty(), this.router.navigateByUrl('/products')));
  }
}
