import { TestBed } from '@angular/core/testing';

import { AmindaService } from './aminda.service';

describe('AmindaService', () => {
  let service: AmindaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AmindaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
