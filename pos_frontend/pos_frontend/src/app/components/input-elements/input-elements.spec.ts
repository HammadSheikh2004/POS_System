import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InputElements } from './input-elements';

describe('InputElements', () => {
  let component: InputElements;
  let fixture: ComponentFixture<InputElements>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InputElements]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InputElements);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
