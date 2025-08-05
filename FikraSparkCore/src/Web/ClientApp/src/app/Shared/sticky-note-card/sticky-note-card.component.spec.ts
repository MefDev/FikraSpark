import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StickyNoteCardComponent } from './sticky-note-card.component';

describe('StickyNoteCardComponent', () => {
  let component: StickyNoteCardComponent;
  let fixture: ComponentFixture<StickyNoteCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StickyNoteCardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StickyNoteCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
