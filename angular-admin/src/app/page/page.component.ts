import { Component, OnInit } from '@angular/core';
import { NbMenuService } from '@nebular/theme';
import { filter } from 'rxjs';
import { SignalrService } from 'src/core/services/signalr.service';

@Component({
  selector: 'app-page',
  templateUrl: './page.component.html',
  styleUrls: ['./page.component.scss']
})
export class PageComponent implements OnInit {
  constructor(
    private menuService: NbMenuService, private _signalr: SignalrService) {
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
    setTimeout(async () => {
      await this._signalr.startConnection();
      //await this._signalr.getConnectionId();
      this._signalr.listenerMessage();
      this._signalr.listenerCall();
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
