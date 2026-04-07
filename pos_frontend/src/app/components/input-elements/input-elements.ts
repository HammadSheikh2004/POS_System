import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter, output } from '@angular/core';
import { FormControl, FormControlName, ReactiveFormsModule } from '@angular/forms';
@Component({
  selector: 'app-input-elements',
  imports: [
    ReactiveFormsModule,
    CommonModule,
  ],
  templateUrl: './input-elements.html',
  styleUrl: './input-elements.css',
})
export class InputElements {
  @Input() label!: string;
  @Input() type: string = '';
  @Input() placeholder: string = '';
  @Input() control!: FormControl;

  @Output() valueChange = new EventEmitter<string>();

  onInputChange(event: any) {
    this.valueChange.emit(event.target.value);
  }
}
