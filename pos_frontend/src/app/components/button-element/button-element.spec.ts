import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ButtonElement } from './button-element';

describe('ButtonElement', () => {
  let component: ButtonElement;
  let fixture: ComponentFixture<ButtonElement>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ButtonElement]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ButtonElement);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
