import { Component, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';
import { faBars } from '@fortawesome/free-solid-svg-icons';
import { FaIconComponent } from '@fortawesome/angular-fontawesome';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [FaIconComponent],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header {
  constructor(private router: Router) {}
  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/']);
  }
  @Output() menuClick = new EventEmitter<void>();

  faBars = faBars;
  sideBarOpen = false;

  toggleSidebar() {
    this.sideBarOpen = !this.sideBarOpen;
  }
}
