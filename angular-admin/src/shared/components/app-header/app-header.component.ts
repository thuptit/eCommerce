import { Component } from '@angular/core';
import { NbMenuService, NbSidebarService, NbThemeService } from '@nebular/theme';
import { filter, flatMap, map, switchMap } from 'rxjs';
import { UserAuth } from 'src/core/models/user-auth.model';
import { TokenAuthService } from 'src/core/services/token-auth.service';

@Component({
  selector: 'app-app-header',
  templateUrl: './app-header.component.html',
  styleUrls: ['./app-header.component.scss']
})
export class AppHeaderComponent {
  userInfo = {} as UserAuth;
  items = [
    { title: 'Profile' },
    { title: 'Logout' },
  ];
  selectedTheme: string = 'default';
  constructor(private tokenAuth: TokenAuthService,
    private themeService: NbThemeService,
    private menuService: NbMenuService,
    private sidebarService: NbSidebarService
  ) {
    this.themeService.changeTheme(this.selectedTheme);
  }
  ngOnInit() {
    this.getUserInfo();
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
  changeTheme() {
    this.themeService.changeTheme(this.selectedTheme);
  }
  collapse() {
    this.sidebarService.getSidebarState('nb-sidebar')
      .subscribe(state => {
        if (state === 'compacted') {
          this.sidebarService.expand('nb-sidebar');
        }
        else if (state === 'expanded') {
          this.sidebarService.compact('nb-sidebar');
        }
      });
  }
}
