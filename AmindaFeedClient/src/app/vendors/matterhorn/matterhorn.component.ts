import {Component, OnInit} from '@angular/core';
import {map} from 'rxjs/operators'
import {TabViewModule} from 'primeng/tabview';
import {ImageModule} from 'primeng/image';
import {GalleriaModule} from 'primeng/galleria';
import {TableModule} from 'primeng/table';
import {ButtonModule} from "primeng/button";
import {ProgressSpinnerModule} from 'primeng/progressspinner';
import { TagModule } from 'primeng/tag';

import {MatterhornService} from './service/matterhorn.service';
import {MatterhornProduct} from './models/mattterhorn-model';
import { Category } from './models/category-model';
import { AmindaService } from '../../Services/aminda.service';

@Component({
  selector: 'app-matterhorn',
  standalone: true,
  imports: [TabViewModule,
      ImageModule, 
      GalleriaModule, 
      TableModule, 
      ButtonModule, 
      ProgressSpinnerModule,
      TagModule,
  ],
  templateUrl: './matterhorn.component.html',
  styleUrl: './matterhorn.component.scss'
})
export class MatterhornComponent implements OnInit {
onAddSelection() {
throw new Error('Method not implemented.');
}
  public matProducts: MatterhornProduct[] = [];
  displayBasic2: boolean = false;
  displayCustom: boolean | undefined;
  galeryImages: string[] = [];
  selectedProducts!: MatterhornProduct[];
  isLoading: boolean = false;
  tabIndex: number = 0;
  categories:Category[]=[
   new Category('Sport',357),
   new Category('Evening Dresses',121),
   new Category('Formal Dresses, Cocktail Dresses',301),
   new Category('Day Dresses',42),
   new Category('Shapewear Bodies for Women',160),
   new Category('Women`s Tops, T-shirts, Singlets',120),
   new Category('Undershirts / Tops',66),
   new Category('Women`s Blouses, Tunics',159),
   new Category('Shirts for Women',41)
  ]

  constructor(
    private readonly matService: MatterhornService,
    private readonly amindaService: AmindaService
  ) {
  }

  ngOnInit(): void {
    console.log("matProducts", this.matProducts)
  }

  createNewMatterhornProduct(prod: any): void {

    var newProd = new MatterhornProduct(
      prod["id"],
      prod["name"],
      prod["url"],
      prod["images"],
      Math.round(prod["prices"]["HUF"]) + 3000,
      Math.round(((prod["prices"]["HUF"] + 3000) * 2 / 1000) * 1000),
      prod["variants"]
    )
    this.matProducts.push(newProd);
  }

  onImageClick(index: number) {
    this.galeryImages = [...this.matProducts[index].images]
    this.displayBasic2 = true
  }

  retreiveDataFromMatterhorn(id: number) {
    this.isLoading = true;
    this.matService.getProductByCategory(id)
      .pipe(map((prods: MatterhornProduct[]) => {
        console.log("fullProduct", prods)
        prods.map((prod) => this.createNewMatterhornProduct(prod))
        return prods;
      }))
      .subscribe(prods => {
        this.isLoading = false;
        // console.log("product : ", prods);
      })
  }

  onAdd() {
    const selectedProductIds = this.selectedProducts.map(prod=>prod.id)
    this.amindaService.addItemsToSelectedList(selectedProductIds)
    console.log("selected: ",this.amindaService.selectedItems)
  }

  onCategorySelect(id: number){
    this.retreiveDataFromMatterhorn(id)
  }
}
