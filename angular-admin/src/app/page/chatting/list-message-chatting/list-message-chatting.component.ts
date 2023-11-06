import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'app-list-message-chatting',
  templateUrl: './list-message-chatting.component.html',
  styleUrls: ['./list-message-chatting.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ListMessageChattingComponent {
  readonly tableData = {
    columns: ['First Name', 'Last Name', 'Age'],
    rows: [
      { firstName: 'Robert', lastName: 'Baratheon', age: 46 },
      { firstName: 'Jaime', lastName: 'Lannister', age: 31 },
    ],
  };

  messages: any[] = [];

  ngOnInit(): void {
    this.loadMessages();
  }

  sendMessage(event: any): void {
    this.messages.push({
      text: event.message,
      date: new Date(),
      reply: true,
      type: 'text',
      user: {
        name: 'Gandalf the Grey',
        avatar: 'https://i.gifer.com/no.gif',
      },
    });
  }

  private loadMessages(): void {
    this.messages = [
      {
        type: 'text',
        text: 'Now you able to use links!',
        customMessageData: {
          href: 'https://akveo.github.io/nebular/',
          text: 'Go to Nebular',
        },
        reply: false,
        date: new Date(),
        user: {
          name: 'Frodo Baggins',
          avatar: 'https://i.gifer.com/no.gif',
        },
      },
      {
        type: 'text',
        customMessageData: {
          href: 'https://akveo.github.io/ngx-admin/',
          text: 'Go to ngx-admin',
        },
        reply: true,
        date: new Date(),
        user: {
          name: 'Meriadoc Brandybuck',
          avatar: 'https://i.gifer.com/no.gif',
        },
      },
      {
        type: 'text',
        customMessageData: 'Click to scroll down',
        reply: false,
        date: new Date(),
        user: {
          name: 'Gimli Gloin',
          avatar: '',
        },
      },
    ]
  }
}
