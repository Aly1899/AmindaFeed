import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs/operators'
import { TabViewModule } from 'primeng/tabview';
import { ImageModule } from 'primeng/image';
import { GalleriaModule } from 'primeng/galleria';

import { MatterhornService } from './service/matterhorn.service';
import { MatterhornProduct } from './models/mattterhorn-model';

@Component({
  selector: 'app-matterhorn',
  standalone: true,
  imports: [TabViewModule, ImageModule, GalleriaModule],
  templateUrl: './matterhorn.component.html',
  styleUrl: './matterhorn.component.scss'
})
export class MatterhornComponent implements OnInit {
  public matProducts:MatterhornProduct[]=[];
  displayBasic2: boolean = false;
  displayCustom: boolean | undefined;
  constructor(
    private readonly matService: MatterhornService,
    
    ){}
  
  ngOnInit(): void {
    this.matService.getProductByCategory(42)
    .pipe(map((prods)=>{
      prods.map((prod)=>this.createNewMatterhornProduct(prod))
      console.log(this.matProducts);
      
      return prods;
    }))
    .subscribe(prods=>{
      // console.log("product : ", prods);
    })
  }

  createNewMatterhornProduct(prod:any):any{
    
    var newProd = new MatterhornProduct(
      prod["id"],
      prod["name"],
      prod["url"],
      prod["images"]
      )
    console.log("new prod :", newProd);
    this.matProducts.push(newProd);
  }
  
}
