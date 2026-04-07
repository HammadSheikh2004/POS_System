import { ChangeDetectorRef, Component } from '@angular/core';
import { Cards } from '../../components/cards/cards';
import {
  faCartFlatbed,
  faDollarSign,
  faUserAlt,
} from '@fortawesome/free-solid-svg-icons';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ChartConfiguration, ChartData, ChartOptions } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { Inventoryservice } from '../../services/inventory/inventoryservice';
import { SaleService } from '../../services/saleservices/sale-service';

@Component({
  selector: 'app-admin-dashboard',
  imports: [BaseChartDirective, Cards, RouterModule, CommonModule],
  templateUrl: './admin-dashboard.html',
  styleUrl: './admin-dashboard.css',
})
export class AdminDashboard {
  trollyIcon = faCartFlatbed;
  salesIcon = faDollarSign;
  customerIcon = faUserAlt;
  constructor(
    private inventoryService: Inventoryservice,
    private cdr: ChangeDetectorRef,
    private saleServive: SaleService
  ) {}
  chartType: 'bar' = 'bar';
  chartData: ChartConfiguration<'bar'>['data'] = {
    labels: [],
    datasets: [
      { data: [], label: 'Available Stock', backgroundColor: '#42A5F5' },
      { data: [], label: 'Sold Stock', backgroundColor: '#FFA726' },
    ],
  };
  chartOptions: ChartConfiguration<'bar'>['options'] = {
    responsive: true,
    plugins: {
      legend: { display: true, position: 'top' },
      title: {
        display: true,
        text: 'Product Stock Overview',
      },
    },
  };

  pieChartData: ChartData<'doughnut'> = {
    labels: [],
    datasets: [
      {
        data: [],
        backgroundColor: [
          '#4CAF50',
          '#2196F3',
          '#FFC107',
          '#F44336',
          '#9C27B0',
        ],
        borderWidth: 1,
      },
    ],
  };
  pieChartOptions: ChartOptions<'doughnut'> = {
    responsive: true,
    plugins: {
      legend: {
        position: 'top',
      },
      title: {
        display: true,
        text: 'Product-wise Sales Distribution',
      },
    },
  };
  ngOnInit(): void {
    this.inventoryService.displayProducts().subscribe((products) => {
      const availableStock = products.map((product) => product.quantityInStock);
      const reorderLevel = products.map((product) => product.reorderLevel);
      const productNames = products.map((product) => product.productName);
      this.chartData.labels = productNames;
      this.chartData.datasets[0].data = availableStock;
      this.chartData.datasets[1].data = reorderLevel;

      this.chartData = {
        labels: productNames,
        datasets: [
          {
            data: availableStock,
            label: 'Available Stock',
            backgroundColor: '#42A5F5',
          },
          {
            data: reorderLevel,
            label: 'Sold Stock',
            backgroundColor: '#FFA726',
          },
        ],
      };
      this.cdr.detectChanges();

      this.saleServive.DisplayService().subscribe({
        next: (response: any) => {
          const salesArray = response.data || []; // 👈 Access actual array here

          const productSale: { [key: string]: number } = {};

          salesArray.forEach((sale: any) => {
            const item = sale.saleItemsDto;
            if (!item) return;

            const productName = `Product ${item.productId}`;
            const total = item.totalPrice || 0;
            productSale[productName] = (productSale[productName] || 0) + total;
          });

          // ✅ Update chart cleanly
          this.pieChartData = {
            labels: Object.keys(productSale),
            datasets: [
              {
                data: Object.values(productSale),
                backgroundColor: [
                  '#4CAF50',
                  '#2196F3',
                  '#FFC107',
                  '#F44336',
                  '#9C27B0',
                  '#00BCD4',
                  '#E91E63',
                ],
                borderWidth: 1,
              },
            ],
          };
          this.cdr.detectChanges();
        },
        error: (err) => console.error('Error loading sales data:', err),
      });
    });
  }
}
