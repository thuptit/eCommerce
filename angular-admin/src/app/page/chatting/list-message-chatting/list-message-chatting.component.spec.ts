import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListMessageChattingComponent } from './list-message-chatting.component';

describe('ListMessageChattingComponent', () => {
  let component: ListMessageChattingComponent;
  let fixture: ComponentFixture<ListMessageChattingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ListMessageChattingComponent]
    });
    fixture = TestBed.createComponent(ListMessageChattingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
