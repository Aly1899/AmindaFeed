import { TestBed } from '@angular/core/testing';

import { MatterhornServiceService } from './matterhorn-service.service';

describe('MatterhornServiceService', () => {
  let service: MatterhornServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MatterhornServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
