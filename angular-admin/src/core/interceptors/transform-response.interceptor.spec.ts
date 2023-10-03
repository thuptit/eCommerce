import { TestBed } from '@angular/core/testing';

import { TransformResponseInterceptor } from './transform-response.interceptor';

describe('TransformResponseInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      TransformResponseInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: TransformResponseInterceptor = TestBed.inject(TransformResponseInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
