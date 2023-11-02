import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListUserChattingComponent } from './list-user-chatting.component';

describe('ListUserChattingComponent', () => {
  let component: ListUserChattingComponent;
  let fixture: ComponentFixture<ListUserChattingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ListUserChattingComponent]
    });
    fixture = TestBed.createComponent(ListUserChattingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
