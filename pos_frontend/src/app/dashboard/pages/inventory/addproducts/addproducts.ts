import { Component, OnInit, signal, Signal } from '@angular/core';
import { Heading } from '../../../../components/heading/heading';
import {
  FormArray,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Inventoryservice } from '../../../../services/inventory/inventoryservice';
import { Displayproducts } from '../displayproducts/displayproducts';

@Component({
  selector: 'app-addproducts',
  standalone: true,
  imports: [
    Heading,
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
    Displayproducts,
  ],
  templateUrl: './addproducts.html',
})
export class Addproducts implements OnInit {
  constructor(private inventoryService: Inventoryservice) {}

  productRows = signal<number[]>([0]);
  productForm!: FormGroup;

  ngOnInit(): void {
    this.productForm = new FormGroup({
      productRow: new FormArray([this.createProductFormGroup()]),
    });
  }

  createProductFormGroup(): FormGroup {
    return new FormGroup({
      productName: new FormControl(''),
      brandName: new FormControl(''),
      unitName: new FormControl(''),
      categoryName: new FormControl(''),
      sellingPrice: new FormControl('', [
        Validators.required,
        Validators.min(1),
        Validators.pattern('^[0-9]*$'),
      ]),
      costPrice: new FormControl('', [
        Validators.required,
        Validators.min(1),
        Validators.pattern('^[0-9]*$'),
      ]),
      reorderLevel: new FormControl(''),
      quantityInStock: new FormControl('', [
        Validators.required,
        Validators.min(1),
        Validators.pattern('^[0-9]*$'),
      ]),
    });
  }

  get productRow(): FormArray {
    return this.productForm.get('productRow') as FormArray;
  }

  addRowProduct(event: Event) {
    event.preventDefault();
    this.productRow.push(this.createProductFormGroup());
    const newRow = [...this.productRows(), this.productRows().length];
    this.productRows.set(newRow);
    if (this.productRows().length >= 5) {
      const table = document.getElementById('productTable');
      table?.classList.add('max-h-[400px]', 'overflow-y-auto');
    }
  }

  DeleteRow(i: number) {
    if(this.productRows().length > 1){
      const updatedRow = [...this.productRows()];
      updatedRow.splice(i,1);
      this.productRow.removeAt(i);
      this.productRows.set(updatedRow);
    }
  }

  submitForm() {
    if (this.productForm.valid) {
      const product = this.productRow.value;
      this.inventoryService.addProduct(product).subscribe(
        (response) => {
          this.productForm.reset();
          this.productRow.clear();
          this.productRows.set([0]);
          this.inventoryService.fetchProducts();
        },
        (error) => {
          console.log(error);
        },
      );
    }
  }
}
