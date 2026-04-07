import { Component, OnInit, signal } from '@angular/core';
import { Heading } from '../../../../components/heading/heading';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CustomerService } from '../../../../services/customerservice/customer-service';

@Component({
  selector: 'app-customer-on-page-report',
  imports: [Heading, ReactiveFormsModule],
  templateUrl: './customer-on-page-report.html',
  styles: ``,
})
export class CustomerOnPageReport implements OnInit {
  constructor(private CustomerService: CustomerService) {}
  OnPageReportForm!: FormGroup;
  OnPageReportTable = signal<any[]>([]);

  ngOnInit(): void {
    this.OnPageReportForm = new FormGroup({
      date_one: new FormControl(),
      date_two: new FormControl(),
      customer_name: new FormControl(),
    });
  }
  loading = signal<boolean>(false)

  OnSubmit() {
    this.loading.set(true);
    let date_one_vale = this.OnPageReportForm.value.date_one;
    let date_two_vale = this.OnPageReportForm.value.date_two;

    let formatted_date_one = new Date(date_one_vale)
      .toISOString()
      .split('T')[0];
    let formatted_date_two = new Date(date_two_vale)
      .toISOString()
      .split('T')[0];

    if (this.OnPageReportForm.valid) {
      let CustomerOnPageReportDTO = {
        DateOne: formatted_date_one,
        DateTwo: formatted_date_two,
        CustomerName: this.OnPageReportForm.value.customer_name,
      };

      const date_one = CustomerOnPageReportDTO.DateOne;
      const date_two = CustomerOnPageReportDTO.DateTwo;
      const customer = CustomerOnPageReportDTO.CustomerName;

      this.CustomerService.OnPageReport(date_one, date_two, customer).subscribe(
        {
          next: (res) => {
            this.OnPageReportTable.set(res);
            this.loading.set(false);
          },
          error: (err) => {
            console.log(err);
            this.loading.set(false);
          },
          complete: () => {
            this.loading.set(false);
          },
        }
      );
    } else {
      this.loading.set(false); 
    }
  }
}
