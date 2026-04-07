import { Component, signal } from '@angular/core';
import { SaleService } from '../../../../services/saleservices/sale-service';
import { Heading } from '../../../../components/heading/heading';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-customersalereport',
  imports: [Heading, DatePipe],
  templateUrl: './customersalereport.html',
  styles: ``,
})
export class Customersalereport {
  customerSaleReport = signal<any[]>([]);
  constructor(private SaleService: SaleService) {
    SaleService.CustomerSaleReport().subscribe({
      next: (res: any) => {
        this.customerSaleReport.set(Array.isArray(res) ? res : res.data);
      },
      error(err) {
        console.log(err);
      },
    });
  }
  async PrintCustomerSaleReport() {
    const customerSaleReportContainer = document.getElementById(
      'customerSaleReportContainer'
    );
    if (!customerSaleReportContainer) return;
    const html2pdf = (await import('html2pdf.js')).default;
    html2pdf().from(customerSaleReportContainer).save('CustomerSaleReport.pdf');
  }
}
