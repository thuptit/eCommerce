import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrUpdateUserComponent } from './create-or-update-user.component';

describe('CreateOrUpdateUserComponent', () => {
  let component: CreateOrUpdateUserComponent;
  let fixture: ComponentFixture<CreateOrUpdateUserComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateOrUpdateUserComponent]
    });
    fixture = TestBed.createComponent(CreateOrUpdateUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
