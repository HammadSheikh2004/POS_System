import { Component } from '@angular/core';
import { Cards } from "../../../../components/cards/cards";
import { faCartFlatbed, faDollarSign, faTruck } from '@fortawesome/free-solid-svg-icons';
import { Heading } from "../../../../components/heading/heading";

@Component({
  selector: 'app-inventorymainpage',
  imports: [Cards, Heading],
  templateUrl: './inventorymainpage.html',
  styles: ``
})
export class Inventorymainpage {
  trollyIcon = faCartFlatbed;
  truckIcon = faTruck;
 
}
