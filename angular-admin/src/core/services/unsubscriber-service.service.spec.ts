import { TestBed } from '@angular/core/testing';

import { UnsubscriberServiceService } from './unsubscriber-service.service';

describe('UnsubscriberServiceService', () => {
  let service: UnsubscriberServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UnsubscriberServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
