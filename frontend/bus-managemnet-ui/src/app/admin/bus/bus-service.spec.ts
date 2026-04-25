import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BusService } from './bus-service';

describe('BusService', () => {
  let component: BusService;
  let fixture: ComponentFixture<BusService>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BusService]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BusService);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
