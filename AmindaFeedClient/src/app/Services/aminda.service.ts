import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AmindaService {
  public selectedItemsToFeed$ = new Subject<number[]>()
  public selectedItems:number[] =[] 
  constructor() { }

  addItemsToSelectedList(items: number[]){
   this.selectedItems.splice(0,0,...items)
   this.selectedItems = [...Array.from(new Set(this.selectedItems))]
  }

  removeItemFromSelectedList(item: number){
    const index=this.selectedItems.indexOf(item);
    this.selectedItems.splice(index,1)
  }

}
