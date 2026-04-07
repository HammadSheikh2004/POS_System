import { TestBed } from '@angular/core/testing';

import { Inventoryservice } from './inventoryservice';

describe('Inventoryservice', () => {
  let service: Inventoryservice;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Inventoryservice);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
