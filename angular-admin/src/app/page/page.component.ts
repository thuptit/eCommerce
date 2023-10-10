import { Component, OnInit } from '@angular/core';
import { NbMenuService } from '@nebular/theme';
import { UserAuth } from 'src/core/models/user-auth.model';
import { TokenAuthService } from 'src/core/services/token-auth.service';

@Component({
  selector: 'app-page',
  templateUrl: './page.component.html',
  styleUrls: ['./page.component.scss']
})
export class PageComponent implements OnInit {
  userInfo = {} as UserAuth;
  constructor(private tokenAuth: TokenAuthService, private menuService: NbMenuService) {
  }
  ngOnInit(): void {
    this.getUserInfo();
    this.menuService.onItemClick()
      .subscribe(event => {
        this.deselected();
        event.item.selected = true;
        return event;
      });
  }

  getUserInfo() {
    this.userInfo = this.tokenAuth.getUser();
  }

  private deselected() {
    this.menuService.getSelectedItem().subscribe(event => {
      event.item.selected = false;
    })
  }
}
