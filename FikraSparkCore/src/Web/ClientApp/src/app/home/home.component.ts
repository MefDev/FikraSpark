import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  mouseX = 0;
  mouseY = 0;

  features = [
    {
      icon: '<i class="ph ph-note-pencil text-yellow-500 text-3xl"></i>',
      title: ' Ideas That Stick, Teams That Sync',
      description: "Post sticky-note ideas, upvote what matters, and stay aligned. FikraSpark is your team's lightweight board for visual collaboration and idea prioritization."
    },
    {
      icon: '<i class="ph ph-faders-horizontal text-indigo-500 text-3xl"></i>',
      title: 'Built for Productive Teams',
      description: 'FikraSpark helps teams gather, vote, and sort ideas on a collaborative sticky-note board — perfect for product planning, feedback collection, or team retrospectives.'
    },
    {
      icon: '<i class="ph ph-sparkle text-pink-500 text-3xl"></i>',
      title: 'Where Big Ideas Begin',
      description: 'Turn sparks into strategy. FikraSpark lets your team post ideas as sticky notes, vote them up, and filter the noise — no meetings needed.'
    },
     
  ];

  stats = [
    { number: '💡', label: 'Capture ideas visually' },
    { number: '👍', label: 'Upvote to prioritize' },
    { number: '⚡', label: 'Fast & Intuitive UI' },
    { number: '🔍', label: 'Filter Top Ideas Instantly' },
  ];

  testimonials = [
    {
      avatar: '👨‍💼',
      name: 'Sarah Chen',
      role: 'Product Manager at TechCorp',
      quote: 'FikraSpark has transformed how our distributed team collaborates. We can brainstorm, plan, and execute all in one place.'
    },
    {
      avatar: '👩‍🎨',
      name: 'Marcus Johnson',
      role: 'Design Lead at StartupXYZ',
      quote: 'The infinite canvas and real-time collaboration features make design thinking sessions incredibly productive.'
    },
    {
      avatar: '👨‍💻',
      name: 'Emily Rodriguez',
      role: 'Scrum Master at DevCo',
      quote: 'Our sprint planning and retrospectives are so much more engaging now. The team loves the interactive experience.'
    }
  ];

  ngOnInit() {
    if (typeof window !== 'undefined') {
      window.addEventListener('mousemove', (e) => {
        this.mouseX = (e.clientX - window.innerWidth / 2) * 0.5;
        this.mouseY = (e.clientY - window.innerHeight / 2) * 0.5;
      });
    }
  }

  onMouseMove(event: MouseEvent) {
    const rect = (event.currentTarget as HTMLElement).getBoundingClientRect();
    this.mouseX = (event.clientX - rect.left - rect.width / 2) * 0.5;
    this.mouseY = (event.clientY - rect.top - rect.height / 2) * 0.5;
  }

  onFeatureHover(feature: any) {
    console.log('Hovering over feature:', feature.title);
  }
}