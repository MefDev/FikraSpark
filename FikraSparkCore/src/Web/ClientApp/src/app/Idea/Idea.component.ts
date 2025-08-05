import { Component, OnInit } from '@angular/core';
import { IdeasClient, IdeaDto, CreateIdeaCommand, UpdateIdeaCommand, VotesClient, VoteIdeaCommand } from '../web-api-client';
import { trigger, transition, style, animate } from '@angular/animations';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';


@Component({
  selector: 'app-Idea-component',
  templateUrl: './Idea.component.html',
  styleUrls: ['./Idea.component.scss'],
  animations: [
    trigger('fadeIn', [
      transition(':enter', [
        style({ opacity: 0, transform: 'scale(0.9)' }),
        animate('300ms ease-out', style({ opacity: 1, transform: 'scale(1)' }))
      ])
    ])
  ]
})

export class IdeaComponent implements OnInit {
  debug = false;
  dragEnabled = false;
  sort: 'latest' | 'top' = 'latest';
  ideas: IdeaDto[] = [];
  selectedIdea: IdeaDto;
  
  
  // Form editors
  newIdeaEditor: any = {};
  ideaEditor: any = {};
  
  // Modal states
  showNewIdeaModal = false;
  showEditIdeaModal = false;
  showDeleteIdeaModal = false;

  // Card colors for sticky notes
  cardColors = ['bg-yellow-100', 'bg-blue-100', 'bg-pink-100', 'bg-green-100', 'bg-purple-100'];
  
  // Store idea styling
  ideaStyling: Map<number, { color: string, rotation: string }> = new Map();

  constructor(
    private ideasClient: IdeasClient,
    private votesClient: VotesClient
  
  ) {}

  ngOnInit(): void {
    this.loadIdeas();
  }

  loadIdeas(): void {
    this.ideasClient.getIdeas(1, 10, this.sort).subscribe(
      result => {
        this.ideas = result.items || [];
        // Assign random styling to each idea
        this.ideas.forEach(idea => {
          if (!this.ideaStyling.has(idea.id)) {
            this.ideaStyling.set(idea.id, {
              color: this.getRandomColor(),
              rotation: this.getRandomRotation()
            });
          }
        });
      },
      error => console.error(error)
    );
  }

  getRandomColor(): string {
    return this.cardColors[Math.floor(Math.random() * this.cardColors.length)];
  }

  getRandomRotation(): string {
    const rotation = Math.floor(Math.random() * 5) - 2; // Random between -2 and 2
    return `rotate-${rotation}`;
  }
  
  // Get styling for a specific idea
  getIdeaColor(ideaId: number): string {
    return this.ideaStyling.get(ideaId)?.color || 'bg-yellow-100';
  }
  
  getIdeaRotation(ideaId: number): string {
    return this.ideaStyling.get(ideaId)?.rotation || '';
  }

  showNewIdeaModalHandler(): void {
    this.showNewIdeaModal = true;
    this.newIdeaEditor = {};
    setTimeout(() => {
      const element = document.getElementById('ideaTitle');
      if (element) element.focus();
    }, 100);
  }

  newIdeaCancelled(): void {
    this.showNewIdeaModal = false;
    this.newIdeaEditor = {};
  }

  addIdea(): void {
    if (!this.newIdeaEditor.title?.trim()) {
      return;
    }

    const command = new CreateIdeaCommand();
    command.title = this.newIdeaEditor.title;
    command.description = this.newIdeaEditor.description || '';

    this.ideasClient.createIdea(command).subscribe(
      result => {
        this.loadIdeas();
        this.showNewIdeaModal = false;
        this.newIdeaEditor = {};
      },
      error => {
        console.error('Error creating idea:', error);
      }
    );
  }

  showEditIdeaModalHandler(idea: IdeaDto): void {
    this.selectedIdea = idea;
    this.ideaEditor = {
      id: idea.id,
      title: idea.title,
      description: idea.description
    };
    this.showEditIdeaModal = true;
  }

  updateIdea(): void {
    if (!this.ideaEditor.title?.trim()) {
      return;
    }

    const command = new UpdateIdeaCommand();
    command.id = this.ideaEditor.id;
    command.title = this.ideaEditor.title;
    command.description = this.ideaEditor.description || '';

    this.ideasClient.updateIdea(this.ideaEditor.id, command).subscribe(
      () => {
        this.loadIdeas(); // Reload the list
        this.showEditIdeaModal = false;
        this.ideaEditor = {};
      },
      error => console.error(error)
    );
  }

  editIdeaCancelled(): void {
    this.showEditIdeaModal = false;
    this.ideaEditor = {};
  }

  showDeleteIdeaModalHandler(idea: IdeaDto): void {
    this.selectedIdea = idea;
    this.showDeleteIdeaModal = true;
  }

  deleteIdeaConfirmed(): void {
    if (!this.selectedIdea?.id) return;

    this.ideasClient.deleteIdea(this.selectedIdea.id).subscribe(
      () => {
        this.loadIdeas(); // Reload the list
        this.showDeleteIdeaModal = false;
        this.selectedIdea = null;
      },
      error => console.error(error)
    );
  }

  deleteIdeaCancelled(): void {
    this.showDeleteIdeaModal = false;
    this.selectedIdea = null;
  }
  
  toggleDrag() {
    this.dragEnabled = !this.dragEnabled;
  }

  drop(event: CdkDragDrop<any[]>) {
    if(!this.dragEnabled) return;
    moveItemInArray(this.ideas, event.previousIndex, event.currentIndex);
  }

  toggleSort(): void {
    this.sort = this.sort === 'latest' ? 'top' : 'latest';
    this.loadIdeas();
  }

  upvoteIdea(idea: IdeaDto, event: Event): void {
    event.stopPropagation();
    
    const command = new VoteIdeaCommand();
    command.id = idea.id;
    command.delta = 1;
    
    this.votesClient.postApiVotesVote(command).subscribe(
      () => {
        idea.votes = (idea.votes || 0) + 1;
      },
      error => console.error('Error upvoting idea:', error)
    );
  }
  
  downvoteIdea(idea: IdeaDto, event: Event): void {
    event.stopPropagation();
    
    const command = new VoteIdeaCommand();
    command.id = idea.id;
    command.delta = -1;
    
    this.votesClient.postApiVotesVote(command).subscribe(
      () => {
        idea.votes = (idea.votes || 0) - 1;
      },
      error => console.error('Error downvoting idea:', error)
    );
  }

  
}
