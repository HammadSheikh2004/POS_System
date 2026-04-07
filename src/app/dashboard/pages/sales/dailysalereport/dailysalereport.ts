import { Component, Inject, PLATFORM_ID, signal } from '@angular/core';
import { Heading } from '../../../../components/heading/heading';
import { SaleService } from '../../../../services/saleservices/sale-service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-dailysalereport',
  imports: [Heading, DatePipe],
  templateUrl: './dailysalereport.html',
  styles: ``,
})
export class Dailysalereport {
  daliySale = signal<any[]>([]);
  constructor(
    private SaleService: SaleService,
  ) {
    SaleService.DailySaleReport().subscribe({
      next: (res) => {
        console.log(res);
        this.daliySale.set(res.data);
      },
      error(err) {
        console.log(err);
      },
    });
  }
  async PrintDailySaleReport() {
    const saleDailyReport = document.getElementById('saleDailyReport');
    if (!saleDailyReport) return;

    const html2pdf = (await import('html2pdf.js')).default;
    html2pdf().from(saleDailyReport).save('DailySaleReport.pdf');
  }
}
