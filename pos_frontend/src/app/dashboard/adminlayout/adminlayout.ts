import { Component } from '@angular/core';
import { Header } from '../header/header';
import { RouterOutlet, RouterLink } from '@angular/router';
import { NgClass } from '@angular/common';
import { faArrowDown, faArrowUp } from '@fortawesome/free-solid-svg-icons';
import { FaIconComponent } from "@fortawesome/angular-fontawesome";

@Component({
  selector: 'app-adminlayout',
  imports: [Header, RouterOutlet, NgClass, RouterLink, FaIconComponent],
  templateUrl: './adminlayout.html',
  styleUrl: './adminlayout.css',
})
export class Adminlayout {
  sideBarOpen = false;
  openMenuIndex: number | null = null;
  arrowDown = faArrowDown;
  arrowUp = faArrowUp;

  toggleSidebar() {
    this.sideBarOpen = !this.sideBarOpen;
  }

  toggleMenu(index: number) {
    this.openMenuIndex = this.openMenuIndex === index ? null : index;
  }

  closeSidebar(event: Event) {
  event.stopPropagation();
    this.sideBarOpen = false;
  }

  navItems = [
    { name: 'Dashboard', route: '/dashboard' },
    {
      name: 'Inventory Management',
      subMenu: [
        { name: 'Products', route: '/admin/inventory/addproducts' },
        { name: 'Purchase Orders', route: '/admin/inventory/addpurchaseorder' },
      ],
    },
    {
      name: 'Sales Management',
      route: '/admin/sales',
      subMenu: [
        {
          name: 'Customer Sales Report',
          route: '/admin/sales/customersalereport',
        },
      ],
    },
    {
      name: 'Customer Management',
      route: '/admin/customer/mainpage',
      subMenu: [
        { name: 'Customer Aging Report', route: '/admin/customer/agingreport' },
      ],
    }
  ];
}
