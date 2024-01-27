import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MatterhornComponent } from './matterhorn.component';

describe('MatterhornComponent', () => {
  let component: MatterhornComponent;
  let fixture: ComponentFixture<MatterhornComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MatterhornComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MatterhornComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
