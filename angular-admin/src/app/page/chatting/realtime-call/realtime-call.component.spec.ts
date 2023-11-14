import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RealtimeCallComponent } from './realtime-call.component';

describe('RealtimeCallComponent', () => {
  let component: RealtimeCallComponent;
  let fixture: ComponentFixture<RealtimeCallComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RealtimeCallComponent]
    });
    fixture = TestBed.createComponent(RealtimeCallComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
