import { Component, signal } from '@angular/core';
import { Heading } from '../../../../components/heading/heading';
import { CustomerService } from '../../../../services/customerservice/customer-service';

@Component({
  selector: 'app-customeragingreport',
  imports: [Heading],
  templateUrl: './customeragingreport.html',
  styles: ``,
})
export class Customeragingreport {
  constructor(private customerService: CustomerService) {}
  customerAgingReportData = signal<any[]>([]);
  ngOnInit(): void {
    this.loadCustomerAgingReport();
  }
  loadCustomerAgingReport() {
    this.customerService.CustomerAgingReport().subscribe({
      next: (res) => {
        this.customerAgingReportData.set(res);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
