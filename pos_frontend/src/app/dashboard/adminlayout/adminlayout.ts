import { Component } from '@angular/core';
import { Header } from "../header/header";
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-adminlayout',
  imports: [Header, RouterOutlet],
  templateUrl: './adminlayout.html',
  styleUrl: './adminlayout.css'
})
export class Adminlayout {

}
