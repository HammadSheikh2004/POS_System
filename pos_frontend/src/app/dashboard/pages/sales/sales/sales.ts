import { Component, OnInit, signal } from '@angular/core';
import { Heading } from '../../../../components/heading/heading';
import { EnumServices } from '../../../../services/enumservice/enum-services';
import { Inventoryservice } from '../../../../services/inventory/inventoryservice';
import {
  ReactiveFormsModule,
  FormsModule,
  FormGroup,
  FormArray,
  FormControl,
} from '@angular/forms';
import { SaleService } from '../../../../services/saleservices/sale-service';
import { Displaysales } from '../displaysales/displaysales';
import { RouterLink } from '@angular/router';
import { debounceTime, distinctUntilChanged } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-sales',
  imports: [
    Heading,
    ReactiveFormsModule,
    FormsModule,
    Displaysales,
    RouterLink,
    CommonModule,
  ],
  templateUrl: './sales.html',
  styles: ``,
})
export class Sales implements OnInit {
  constructor(
    private EnumService: EnumServices,
    private InventoryService: Inventoryservice,
    private SaleService: SaleService
  ) {}

  payment_status: { id: number; name: string }[] = [];
  payment_method: { id: number; name: string }[] = [];
  priceCalculation: { quantity: number; unit_price: number }[] = [
    { quantity: 0, unit_price: 0 },
  ];
  products = signal<any[]>([]);
  rows = signal<number[]>([0]);
  total = 0;
  SaleForm!: FormGroup;
  saleCounter: number = 0;
  TodayDate = signal<any[]>([]);
  all_customer = signal<any[]>([]);
  filtered_customer = signal<any>([]);
  show_suggestion: boolean = false;

  ngOnInit(): void {
    this.EnumService.getPaymentStatus().subscribe({
      next: (data) => {
        this.payment_status = data;
      },
      error(err) {
        console.log(err);
      },
    });

    this.EnumService.getPaymentMethos().subscribe({
      next: (data) => {
        this.payment_method = data;
      },
      error(err) {
        console.log(err);
      },
    });

    this.InventoryService.displayProductName().subscribe({
      next: (res) => {
        this.products.set(res);
      },
      error: (err) => {
        console.log(err);
      },
    });

    this.SaleService.DisplayService().subscribe({
      next: (res: any) => {
        let data = Array.isArray(res) ? res : res.data;
        // data.forEach((item: any) => {
        //   this.all_customer.set(item.customerName);
        // });
        this.all_customer.set(data);
      },
      error(err) {
        console.log(err);
      },
    });

    this.SaleForm = new FormGroup({
      customer: new FormGroup({
        CustomerName: new FormControl(''),
        CustomerAddress: new FormControl(''),
        CustomerNumber: new FormControl(''),
      }),
      sales: new FormArray([this.CreateSaleFormField()]),
      totalPrice: new FormControl(0),
      AmountPay: new FormControl(0), 
      DueAmount: new FormControl(0),
    });

    this.SaleForm.get('customer.CustomerName')
      ?.valueChanges.pipe(debounceTime(200), distinctUntilChanged())
      .subscribe({
        next: (value) => {
          if (!value) {
            this.filtered_customer.set([]);
            return;
          }
          const search = value.toLowerCase();
          let filterCustomer = this.all_customer().filter((item: any) => {
            return item.customerName.toLowerCase().includes(search);
          });
          this.filtered_customer.set(filterCustomer);
        },
      });
  }

  SelectCustomer(c: any) {
    this.SaleForm.get('customer.CustomerName')?.setValue(c.customerName);
    this.show_suggestion = false;
  }

  AddRow(event: Event) {
    this.sales.push(this.CreateSaleFormField());
    const updatedRow = [...this.rows(), this.rows().length];
    this.rows.set(updatedRow);
    this.priceCalculation.push({ quantity: 0, unit_price: 0 });
    const row = this.CreateSaleFormField();
    row.get('saleDate')?.setValue(new Date());

    this.TotalPrice();
    if (this.rows().length >= 4) {
      const saleTable = document.getElementById('salesTable');
      saleTable?.classList.add('tableClass');
    }
  }
  DeleteRow(i: number) {
    if (this.rows().length > 1) {
      const updatedRow = [...this.rows()];
      updatedRow.splice(i, 1);
      (this.SaleForm.get('sales') as FormArray).removeAt(i);
      this.rows.set(updatedRow);
      this.TotalPrice();
    }
  }

  GenerateSaleNumber() {
    this.saleCounter++;
    const date = new Date();
    return `Sale_${date.getDate()}_${date.getMonth()}_${date.getFullYear()}_${
      this.saleCounter
    }`;
  }
  CreateSaleFormField(): FormGroup {
    let date = new Date();
    let year = date.getFullYear();
    let month = String(date.getMonth() + 1).padStart(2, '0');
    let day = String(date.getDate()).padStart(2, '0');
    let formattedDate = `${year}-${month}-${day}`;
    return new FormGroup({
      saleNumber: new FormControl(this.GenerateSaleNumber()),
      saleDate: new FormControl(formattedDate),
      paymentStatus: new FormControl(null),
      paymentMethod: new FormControl(null),
      productId: new FormControl<number | null>(null),
      quantity: new FormControl(null),
      unitPrice: new FormControl(null),
      totalPrice: new FormControl(null),
    });
  }

  get sales(): FormArray {
    return this.SaleForm.get('sales') as FormArray;
  }

  TotalPrice() {
    this.total = this.sales.controls.reduce((sum, ctrl) => {
      const q = Number(ctrl.get('quantity')?.value) || 0;
      const p = Number(ctrl.get('unitPrice')?.value) || 0;
      const rowTotal = q * p;

      ctrl.get('totalPrice')?.setValue(rowTotal, { emitEvent: false });

      return sum + rowTotal;
    }, 0);

    if (this.sales.length === 0) {
      this.total = 0;
    }
  }

  amountPay: number = 0;
  dueAmount: number = 0;
  CalculateDueAmount() {
    const total = this.total;
    const paid = this.SaleForm.get('AmountPay')?.value;
    const due = total - paid;

    this.dueAmount = due;
    this.SaleForm.get('DueAmount')?.setValue(due);
  }

  OnSubmit(): void {
    if (this.SaleForm.valid) {
      const saleData = this.SaleForm.value;

      const dto = [
        {
          CustomerName: saleData.customer.CustomerName,
          CustomerAddress: saleData.customer.CustomerAddress,
          CustomerPhoneNumber: saleData.customer.CustomerNumber,
          SaleDateTransferDto: {
            SaleNumber: saleData.sales[0].saleNumber,
            SaleDate: saleData.sales[0].saleDate,
            PaymentStatus: Number(saleData.sales[0].paymentStatus),
            PaymentMethod: Number(saleData.sales[0].paymentMethod),
          },
          SaleItemsDto: {
            ProductId: Number(saleData.sales[0].productId),
            Quantity: Number(saleData.sales[0].quantity),
            UnitPrice: Number(saleData.sales[0].unitPrice),
            TotalPrice: Number(saleData.sales[0].totalPrice),
            AmountPay: Number(saleData.AmountPay),
            DueAmount: Number(saleData.DueAmount),
          },
        },
      ];
      this.SaleService.AddSales(dto).subscribe({
        next: (res: any) => {
          this.SaleForm.reset();
          this.sales.clear();
          this.rows.set([0]);
          this.SaleService.FetchALLSale();
          this.total = 0;
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
  }
  PrintTodayReport() {
    let date = new Date();

    let year = date.getFullYear();
    let month = String(date.getMonth() + 1).padStart(2, '0');
    let day = String(date.getDate()).padStart(2, '0');

    let formattedDate = `${year}-${month}-${day}`;
    this.SaleService.TodaySaleReport(formattedDate).subscribe({
      next: (res: any) => {
        let data = Array.isArray(res) ? res : res.data;
        this.TodayDate.set(data);
        let tableRows = '';
        if (data && data.length > 0) {
          data.forEach((item: any) => {
            tableRows += `
            <tr>
              <td>${item.customerID}</td>
              <td>${item.customerName}</td>
              <td>${item.productID}</td>
              <td>${item.productName}</td>
              <td>${item.customerPhoneNumber}</td>
              <td>${new Date(item.saleDate).toLocaleString()}</td>
              <td>${item.totalQuantity}</td>
              <td>${item.totalRenevue}</td>
            </tr>
          `;
          });
        } else {
          tableRows = `
          <tr>
            <td colspan="8">No Sale Today</td>
          </tr>
        `;
        }
        var TodaySaleReportContainer = `
        <div class="container" id="TodaySaleReportContainer">
          <div class="row my-2 text-center">
            <div class="col-md-12">
              <h2>Today Sale Report</h2>
            </div>
          </div>
          <div class="row">
            <div class="col-md-12">
              <div class="table-responsive">
                <table class="table text-center">
                  <thead>
                    <tr>
                      <th scope="col">Customer Id</th>
                      <th scope="col">Customer Name</th>
                      <th scope="col">Product Id</th>
                      <th scope="col">Product Name</th>
                      <th scope="col">Customer Phone Number</th>
                      <th scope="col">Sale Date</th>
                      <th scope="col">Total Quantity</th>
                      <th scope="col">Total Amount</th>
                    </tr>
                  </thead>
                  <tbody>
                    ${tableRows}
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>
      `;
        import('html2pdf.js').then((module) => {
          let html2pdf = module.default;
          html2pdf().from(TodaySaleReportContainer).save('TodaySaleReport.pdf');
        });
      },
      error(err) {
        console.log(err);
      },
    });
  }
}
