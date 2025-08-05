import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IdeaDto } from 'src/app/web-api-client';

@Component({
  selector: 'app-sticky-note-form',
  standalone: true,
  imports: [],
  templateUrl: './sticky-note-form.component.html',
  styleUrl: './sticky-note-form.component.css'
})
export class StickyNoteFormComponent {
  @Input() isEdit: boolean = false;
  @Input() idea: IdeaDto;
  @Input() visible: boolean = false;

  @Output() cancel = new EventEmitter<void>();
  @Output() submit = new EventEmitter<IdeaDto>();

  handleSubmit() {
    if (this.idea.title.trim()) {
      this.submit.emit(this.idea);
    }
  }
  handleCancel() {
    this.cancel.emit();
  }
}
