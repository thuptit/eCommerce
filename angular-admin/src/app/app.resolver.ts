import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { CommonService } from 'src/core/services/common.service';

export const appResolver: ResolveFn<void> = (route, state) => {
  return inject(CommonService).loadConfiguration();
};
