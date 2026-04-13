import { Component, signal } from '@angular/core';
import { SaleService } from '../../../../services/saleservices/sale-service';
import { Heading } from "../../../../components/heading/heading";

@Component({
  selector: 'app-productwisesalereport',
  imports: [Heading],
  templateUrl: './productwisesalereport.html',
  styles: ``,
})
export class Productwisesalereport {
  productWiseSaleReport = signal<any[]>([]);
  constructor(private SaleService: SaleService) {
    SaleService.ProductWiseSaleReport().subscribe({
      next: (res: any) => {
        let data = Array.isArray(res) ? res : res.data;
        this.productWiseSaleReport.set(data);
        console.log(data);
      },
      error(err) {
        console.log(err);
      },
    });
  }
  async PrintProductWiseSaleReport(){
    const prodcutWiseReportContainer = document.getElementById("prodcutWiseReportContainer");
    if(!prodcutWiseReportContainer) return;
    let html2pdf = (await import('html2pdf.js')).default;
    html2pdf().from(prodcutWiseReportContainer).save("ProductWiseReport.pdf");
  }
}
