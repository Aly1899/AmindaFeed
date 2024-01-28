import { Component } from '@angular/core';
import { TabViewModule } from 'primeng/tabview';

@Component({
  selector: 'app-matterhorn',
  standalone: true,
  imports: [TabViewModule],
  templateUrl: './matterhorn.component.html',
  styleUrl: './matterhorn.component.scss'
})
export class MatterhornComponent {

}
