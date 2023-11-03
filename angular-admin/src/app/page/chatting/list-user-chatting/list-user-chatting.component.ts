import { Component } from '@angular/core';

@Component({
  selector: 'app-list-user-chatting',
  templateUrl: './list-user-chatting.component.html',
  styleUrls: ['./list-user-chatting.component.scss']
})
export class ListUserChattingComponent {
  users: { name: string, title: string }[] = [
    { name: 'Carla Espinosa', title: 'Nurse' },
    { name: 'Bob Kelso', title: 'Doctor of Medicine' },
    { name: 'Janitor', title: 'Janitor' },
    { name: 'Perry Cox', title: 'Doctor of Medicine' },
    { name: 'Ben Sullivan', title: 'Carpenter and photographer' },
  ];
}
