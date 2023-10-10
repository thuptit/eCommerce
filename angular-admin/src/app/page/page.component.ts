import { Component, OnInit } from '@angular/core';
import { NbMenuService } from '@nebular/theme';
import { filter } from 'rxjs';

@Component({
  selector: 'app-page',
  templateUrl: './page.component.html',
  styleUrls: ['./page.component.scss']
})
export class PageComponent implements OnInit {
  constructor(
    private menuService: NbMenuService) {
  }
  ngOnInit(): void {
    this.menuService.onItemClick()
      .pipe(
        filter((event) => event.tag === 'menu-side-bar')
      )
      .subscribe(event => {
        this.deselected();
        event.item.selected = true;
        return event;
      });
  }

  private deselected() {
    this.menuService.getSelectedItem()
      .pipe(
        filter((event) => event.tag === 'menu-side-bar')
      )
      .subscribe(event => {
        event.item.selected = false;
      })
  }
}
