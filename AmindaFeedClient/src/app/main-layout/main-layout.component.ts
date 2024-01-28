import {Component, OnInit} from '@angular/core';
import {MenuItem} from 'primeng/api';
import {MenuModule} from "primeng/menu";
import {CommonModule} from "@angular/common";
import {ContentLayoutComponent} from "../content-layout/content-layout.component";
import {SidebarComponent} from "../sidebar/sidebar.component";

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [
    MenuModule,
    CommonModule,
    ContentLayoutComponent,
    SidebarComponent,
  ],
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.scss'
})
export class MainLayoutComponent {

}
