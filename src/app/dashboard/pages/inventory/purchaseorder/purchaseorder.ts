import { Component, signal } from '@angular/core';
import { Inventoryservice } from '../../../../services/inventory/inventoryservice';
import {
  Form,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { DatePipe } from '@angular/common';
import { Heading } from "../../../../components/heading/heading";

@Component({
  selector: 'app-purchaseorder',
  imports: [ReactiveFormsModule, DatePipe, Heading],
  templateUrl: './purchaseorder.html',
  styles: ``,
})
export class Purchaseorder {
  constructor(private InventoryService: Inventoryservice) {
    this.getProductName();
    this.PurchaseOrderForm = this.PurchaseOrderFields();
    this.InventoryService.displayPurchaseOrders().subscribe({
      next: (res) => {
        this.purchaseOrder.set(res);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
  product = signal<any[]>([]);
  PurchaseOrderForm!: FormGroup;
  purchaseOrder = signal<any[]>([]);

  PurchaseOrderFields(): FormGroup {
    return new FormGroup({
      productName: new FormControl('', [Validators.required]),
      costPricePerItem: new FormControl('', [
        Validators.required,
        Validators.min(1),
        Validators.pattern('^[0-9]*$'),
      ]),
      supplierName: new FormControl('', [Validators.required]),
      items: new FormControl('', [
        Validators.required,
        Validators.min(1),
        Validators.pattern('^[0-9]*$'),
      ]),
    });
  }

  getProductName() {
    this.InventoryService.displayProducts().subscribe({
      next: (res) => {
        this.product.set(res);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  onSubmit(): void {
    if (this.PurchaseOrderForm.valid) {
      console.log("Form Submitted!", this.PurchaseOrderForm.value);
      this.InventoryService.AddPurchaseOrder(
        this.PurchaseOrderForm.value
      ).subscribe({
        next: (res) => {
          console.log(res);
          const updateOrders = [...this.purchaseOrder(), res.result];
          this.purchaseOrder.set(updateOrders);

          alert(res.successMessage);
          this.PurchaseOrderForm.reset();
        },
        error: (err) => {
          alert(err.error.errorMessage);
        },
      });
    }
  }
}
