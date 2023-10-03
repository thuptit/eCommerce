import { TestBed } from '@angular/core/testing';

import { HeaderHttpInterceptor } from './header-http.interceptor';

describe('HeaderHttpInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      HeaderHttpInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: HeaderHttpInterceptor = TestBed.inject(HeaderHttpInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
