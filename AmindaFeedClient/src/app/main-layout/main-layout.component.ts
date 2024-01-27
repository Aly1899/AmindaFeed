import {Component, OnInit} from '@angular/core';
import {MenuItem} from 'primeng/api';
import {MenuModule} from "primeng/menu";
import {CommonModule} from "@angular/common";

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [
    MenuModule,
    CommonModule,
  ],
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.scss'
})
export class MainLayoutComponent implements OnInit {
  items: MenuItem[] | undefined;

  ngOnInit() {
    this.items = [
      {
        label: 'Wholesalers',
        items: [
          {
            label: 'Matterhorn',
            command: () => {
              //navigate to matterhorn page
            }
          }
        ]
      },
      {
        label: 'Settings',
        items: [
          {
            label: 'Setting1',
            icon: 'pi pi-external-link',
          },
          {
            label: 'Setting2',
            icon: 'pi pi-upload',
          }
        ]
      }
    ];
  }

  update() {
  }

  delete() {
  }
}
