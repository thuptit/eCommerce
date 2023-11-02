import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChattingComponent } from './chatting.component';

describe('ChattingComponent', () => {
  let component: ChattingComponent;
  let fixture: ComponentFixture<ChattingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ChattingComponent]
    });
    fixture = TestBed.createComponent(ChattingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
