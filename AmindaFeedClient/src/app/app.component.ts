import {Component} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterOutlet} from '@angular/router';
import {MainLayoutComponent} from "./main-layout/main-layout.component";
import {ContentLayoutComponent} from "./main-layout/content-layout/content-layout.component";
import {SidebarComponent} from "./main-layout/sidebar/sidebar.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, MainLayoutComponent, ContentLayoutComponent, SidebarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'AmindaFeedClient';
}
