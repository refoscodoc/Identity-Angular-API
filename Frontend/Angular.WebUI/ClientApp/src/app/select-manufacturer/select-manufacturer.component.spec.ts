import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectManufacturerComponent } from './select-manufacturer.component';

describe('SelectManufacturerComponent', () => {
  let component: SelectManufacturerComponent;
  let fixture: ComponentFixture<SelectManufacturerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SelectManufacturerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectManufacturerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
