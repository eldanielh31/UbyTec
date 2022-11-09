import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterAffilComponent } from './register-affil.component';

describe('RegisterAffilComponent', () => {
  let component: RegisterAffilComponent;
  let fixture: ComponentFixture<RegisterAffilComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegisterAffilComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisterAffilComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
