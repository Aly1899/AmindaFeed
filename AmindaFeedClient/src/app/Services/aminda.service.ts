import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AmindaService {
  public selectedItemsToFeed$ = new Subject<number[]>()
  public selectedItems:number[] =[] 
  constructor(private httpClient:HttpClient) { }

  addItemsToSelectedList(items: number[]){
   this.selectedItems.splice(0,0,...items)
   this.selectedItems = [...Array.from(new Set(this.selectedItems))]
  }

  removeItemFromSelectedList(item: number){
    const index=this.selectedItems.indexOf(item);
    this.selectedItems.splice(index,1)
  }

  sendItemsToAminda(items: number[]):Observable<any>{
    return this.httpClient.post(`${environment.backendUri}/Product/SetAmindaProductsFromMatterhorn`,items)
  }

}
