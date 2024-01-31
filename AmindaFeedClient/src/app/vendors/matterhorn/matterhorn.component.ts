import {Component, OnInit} from '@angular/core';
import {map} from 'rxjs/operators'
import {TabViewModule} from 'primeng/tabview';
import {ImageModule} from 'primeng/image';
import {GalleriaModule} from 'primeng/galleria';
import {TableModule} from 'primeng/table';
import {ButtonModule} from "primeng/button";
import {ProgressSpinnerModule} from 'primeng/progressspinner';

import {MatterhornService} from './service/matterhorn.service';
import {MatterhornProduct} from './models/mattterhorn-model';

@Component({
  selector: 'app-matterhorn',
  standalone: true,
  imports: [TabViewModule, ImageModule, GalleriaModule, TableModule, ButtonModule, ProgressSpinnerModule],
  templateUrl: './matterhorn.component.html',
  styleUrl: './matterhorn.component.scss'
})
export class MatterhornComponent implements OnInit {
  public matProducts: MatterhornProduct[] = [];
  displayBasic2: boolean = false;
  displayCustom: boolean | undefined;
  galeryImages: string[] = [];
  selectedProducts!: MatterhornProduct;
  isLoading: boolean = false;
  tabIndex: number = 0;

  constructor(
    private readonly matService: MatterhornService,
  ) {
  }

  ngOnInit(): void {
    this.retreiveDataFromMatterhorn()
    console.log("matProducts", this.matProducts)
  }

  createNewMatterhornProduct(prod: any): void {

    var newProd = new MatterhornProduct(
      prod["id"],
      prod["name"],
      prod["url"],
      prod["images"],
      Math.round(prod["prices"]["HUF"]),
      Math.round((prod["prices"]["HUF"] * 2 / 1000) * 1000),
    )
    this.matProducts.push(newProd);
  }

  onImageClick(index: number) {
    this.galeryImages = [...this.matProducts[index].images]
    this.displayBasic2 = true
  }

  retreiveDataFromMatterhorn() {
    this.isLoading = true;
    this.matService.getProductByCategory(42)
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
    console.log(this.selectedProducts)
  }
}
