import { Component } from '@angular/core';
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
      selected: true
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
      title: 'Users',
      link: 'users',
      icon: 'people'
    }
  ]
}
