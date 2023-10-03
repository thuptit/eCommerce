import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class LoggerService {

  constructor(private _toast: ToastrService) { }

  error(message: string) {
    this._toast.error(message, 'Failed');
  }

  info(message: string) {
    this._toast.info(message);
  }

  success(message: string) {
    this._toast.success(message, 'Successfully');
  }

  warn(message: string) {
    this._toast.warning(message);
  }
}
