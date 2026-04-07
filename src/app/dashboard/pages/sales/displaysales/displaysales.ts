import { Component, signal, effect } from '@angular/core';
import { SaleService } from '../../../../services/saleservices/sale-service';
import { Heading } from '../../../../components/heading/heading';
import { DatePipe } from '@angular/common';
import DataTable from 'datatables.net-dt';

@Component({
  selector: 'app-displaysales',
  imports: [Heading, DatePipe],
  templateUrl: './displaysales.html',
})
export class Displaysales {
  sale: any;
  private dataTable: any;

  constructor(private SaleService: SaleService) {
    this.sale = this.SaleService.sale;
    effect(() => {
      const data = this.sale();
      if (data.length > 0) {
        setTimeout(() => {
          if (this.dataTable) {
            this.dataTable.destroy();
          }
          const table = document.getElementById('sale_table');
          if (table) {
            this.dataTable = new DataTable(table);
          }
        }, 100);
      }
    });
  }

  ngOnInit(): void {
    this.SaleService.FetchALLSale(); // sirf data call yahan
  }
}
