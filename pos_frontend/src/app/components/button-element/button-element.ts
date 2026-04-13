import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-button-element',
  standalone:true,
  imports: [],
  templateUrl: './button-element.html',
  styleUrl: './button-element.css',
})
export class ButtonElement {
  @Input() label: string = 'Submit';
  @Input() icon!: string;
  @Input() disable = false;
  @Input() type: string = '';

  @Output() btnClick = new EventEmitter<void>();
  onButtonClick() {
    this.btnClick.emit();
  }
}
