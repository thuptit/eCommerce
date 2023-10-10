import { Component, OnInit } from '@angular/core';
import { NbMenuService } from '@nebular/theme';
import { filter, map } from 'rxjs';
import { UserAuth } from 'src/core/models/user-auth.model';
import { TokenAuthService } from 'src/core/services/token-auth.service';

@Component({
  selector: 'app-page',
  templateUrl: './page.component.html',
  styleUrls: ['./page.component.scss']
})
export class PageComponent implements OnInit {
  userInfo = {} as UserAuth;
  items = [
    { title: 'Profile' },
    { title: 'Logout' },
  ];
  constructor(
    private tokenAuth: TokenAuthService,
    private menuService: NbMenuService) {
  }
  ngOnInit(): void {
    this.getUserInfo();
    this.menuService.onItemClick()
      .pipe(
        filter(({ tag }) => tag !== 'menu-context-user-tag')
      )
      .subscribe(event => {
        this.deselected();
        event.item.selected = true;
        return event;
      });
    this.menuService.onItemClick()
      .pipe(
        filter(({ tag }) => tag === 'menu-context-user-tag'),
        map(({ item: { title } }) => title),
      )
      .subscribe(title => {
        if (title === 'Logout') {
          this.tokenAuth.removeAuthToken();
          window.location.reload();
        }
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
