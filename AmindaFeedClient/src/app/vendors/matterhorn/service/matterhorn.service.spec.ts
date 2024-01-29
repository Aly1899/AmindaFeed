import { TestBed } from '@angular/core/testing';

import { MatterhornService } from './matterhorn.service';

describe('MatterhornServiceService', () => {
  let service: MatterhornService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MatterhornService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
