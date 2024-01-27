import {Component, OnInit} from '@angular/core';
import {MenuModule} from "primeng/menu";
import {MenuItem} from "primeng/api";

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [
    MenuModule
  ],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss'
})
export class SidebarComponent implements OnInit {
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
