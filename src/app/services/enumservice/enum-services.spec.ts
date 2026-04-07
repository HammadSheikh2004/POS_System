import { TestBed } from '@angular/core/testing';

import { EnumServices } from './enum-services';

describe('EnumServices', () => {
  let service: EnumServices;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EnumServices);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
