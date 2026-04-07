import { Component, Input } from '@angular/core';
import { RouterLink, RouterModule } from '@angular/router';
import { FaIconComponent } from '@fortawesome/angular-fontawesome';
import { faBell } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-cards',
  imports: [FaIconComponent,RouterLink,RouterModule],
  templateUrl: './cards.html',
  styleUrl: './cards.css',
})
export class Cards {
  @Input() icon: any;
  @Input() cardTitle: string = '';
  @Input() navigate: string = '';
}
