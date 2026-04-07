import { Component, OnInit, signal } from '@angular/core';
import { Inventoryservice } from '../../../../services/inventory/inventoryservice';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-displayproducts',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './displayproducts.html',
  styles: ``,
})
export class Displayproducts implements OnInit {
  constructor(private InventoryService: Inventoryservice) {}
  products = signal<any[]>([]);
  ngOnInit(): void {
    this.InventoryService.fetchProducts();
    this.products = this.InventoryService.pro;
  }
  successMessage = signal<string>('');

  DeleteProduct(id:number){
    this.InventoryService.deleteProduct(id).subscribe({
      next: (res)=>{
        this.successMessage.set(res.successMessage);
        setTimeout(() => {
          this.successMessage.set('');
        }, 3000);
        this.InventoryService.fetchProducts();
      },
      error:(err)=>console.error(err)
    })
  }
}
