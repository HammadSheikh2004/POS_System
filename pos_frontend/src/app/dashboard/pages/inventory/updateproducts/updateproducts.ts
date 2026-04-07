import { Component, OnInit, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Inventoryservice } from '../../../../services/inventory/inventoryservice';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-updateproducts',
  imports: [ReactiveFormsModule],
  templateUrl: './updateproducts.html',
  styles: ``,
})
export class Updateproducts {
  productId: string = '';
  product = signal<any>(null);
  productForm!: FormGroup;
  constructor(
    private route: ActivatedRoute,
    private InventoryService: Inventoryservice
  ) {
    this.route.params.subscribe((params) => {
      this.productId = params['id'];
      this.InventoryService.fetchProductById(this.productId).subscribe({
        next: (res) => {
          let prod = Array.isArray(res) ? res[0] : 0;
          this.product.set(prod);
          this.productForm = new FormGroup({
            productId: new FormControl(prod.productId),
            productName: new FormControl(prod.productName, Validators.required),
            sellingPrice: new FormControl(
              prod.sellingPrice,
              Validators.required
            ),
            costPrice: new FormControl(prod.costPrice, Validators.required),
            reorderLevel: new FormControl(prod.reorderLevel),
            quantityInStock: new FormControl(prod.quantityInStock, Validators.required),

            brandId: new FormControl(prod.brand?.brandId),
            brandName: new FormControl(prod.brand?.brandName),

            categoryId: new FormControl(prod.category?.categoryId),
            categoryName: new FormControl(prod.category?.categoryName),

            unitId: new FormControl(prod.unit?.unitId),
            unitName: new FormControl(prod.unit?.unitName),
          });
        },
        error: (err) => {
          console.error('Error fetching product:', err);
        },
      });
    });
  }

  onSubmit(): void {
    if (this.productForm.invalid) return;

    const formValue = this.productForm.value;
    const dto = {
      productId: formValue.productId,
      productName: formValue.productName,
      sellingPrice: formValue.sellingPrice,
      costPrice: formValue.costPrice,
      reorderLevel: formValue.reorderLevel,
      quantityInStock: formValue.quantityInStock,
      brand: {
        brandId: formValue.brandId,
        brandName: formValue.brandName,
      },
      category: {
        categoryId: formValue.categoryId,
        categoryName: formValue.categoryName,
      },
      unit: {
        unitId: formValue.unitId,
        unitName: formValue.unitName,
      },
    };

    this.InventoryService.updateProduct(dto, this.productId).subscribe({
      next: (res) => {
        alert('✅ Product updated successfully');
      },
      error: (err) => {
        alert('❌ Error updating product');
      },
    });
  }
}
