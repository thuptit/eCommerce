import { Component, OnInit } from '@angular/core';
import { UserAuth } from 'src/core/models/user-auth.model';
import { TokenAuthService } from 'src/core/services/token-auth.service';

@Component({
  selector: 'app-page',
  templateUrl: './page.component.html',
  styleUrls: ['./page.component.scss']
})
export class PageComponent implements OnInit {
  userInfo = {} as UserAuth;
  constructor(private tokenAuth: TokenAuthService) {
  }
  ngOnInit(): void {
    this.getUserInfo();
  }

  getUserInfo() {
    this.userInfo = this.tokenAuth.getUser();
  }
}
