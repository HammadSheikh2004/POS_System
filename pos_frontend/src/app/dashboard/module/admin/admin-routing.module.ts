import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminDashboard } from '../../admin-dashboard/admin-dashboard';
import { Adminlayout } from '../../adminlayout/adminlayout';
import { Addproducts } from '../../pages/inventory/addproducts/addproducts';
import { Inventorymainpage } from '../../pages/inventory/inventorymainpage/inventorymainpage';
import { Updateproducts } from '../../pages/inventory/updateproducts/updateproducts';
import { Purchaseorder } from '../../pages/inventory/purchaseorder/purchaseorder';
import { Sales } from '../../pages/sales/sales/sales';
import { Dailysalereport } from '../../pages/sales/dailysalereport/dailysalereport';
import { Customersalereport } from '../../pages/sales/customersalereport/customersalereport';
import { Productwisesalereport } from '../../pages/sales/productwisesalereport/productwisesalereport';
import { Mainpage } from '../../pages/customers/mainpage/mainpage';
import { CustomerOnPageReport } from '../../pages/customers/customer-on-page-report/customer-on-page-report';
import { Customeragingreport } from '../../pages/customers/customeragingreport/customeragingreport';

export const Admin_Routes: Routes = [
  {
    path: '',
    component: Adminlayout,
    children: [
      { path: 'dashboard', component: AdminDashboard },
      

      {
        path: 'inventory',
        component: Inventorymainpage,
      },
      {
        path: 'inventory/addproducts',
        component: Addproducts,
      },
      {
        path: 'inventory/updateproduct/:id',
        component: Updateproducts,
      },
      {
        path: 'inventory/addpurchaseorder',
        component: Purchaseorder,
      },
      {
        path: 'sales',
        component: Sales,
      },
      {
        path: 'sales/dailysalereport',
        component: Dailysalereport,
      },
      {
        path: 'sales/customersalereport',
        component: Customersalereport,
      },
      {
        path: 'sales/productwisesalereport',
        component: Productwisesalereport,
      },
      {
        path: 'customer/mainpage',
        component: Mainpage,
      },
      {
        path: 'customer/onpagereport',
        component: CustomerOnPageReport,
      },
      {
        path: 'customer/agingreport',
        component: Customeragingreport,
      },

      { path: '', redirectTo: 'dashboard', pathMatch: 'full' }, 
      
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(Admin_Routes)],
  exports: [RouterModule],
})
export class AdminRoutingModule {}
