import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NbMenuItem } from '@nebular/theme';

@Component({
  selector: 'app-menu-bar',
  templateUrl: './menu-bar.component.html',
  styleUrls: ['./menu-bar.component.scss']
})
export class MenuBarComponent {
  menuItems: NbMenuItem[] = [
    {
      title: 'Home',
      link: 'home',
      icon: 'home',
    },
    {
      title: 'Category',
      link: 'category',
      icon: 'credit-card',
    },
    {
      title: 'Product',
      link: 'product',
      icon: 'gift'
    },
    {
      title: 'Chat',
      link: 'chat',
      icon: 'message-circle-outline'
    },
    {
      title: 'Users',
      link: 'users',
      icon: 'people'
    }
  ];
  constructor(private _router: Router) {
  }
  ngOnInit() {
    this.getSelectedItem();
  }

  private getSelectedItem() {
    this.menuItems.forEach(element => {
      if (this._router.url.startsWith('/eCommerce/' + element.link)) {
        element.selected = true;
      }
    })
  }
}
