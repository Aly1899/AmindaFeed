export class MatterhornProduct {
  id: number;
  name: string;
  url: string;
  images: string[];
  purchasePrice: number;
  salePrice: number;

  constructor(id: number, name: string, url: string, images: string[],
              purchasePrice: number, salePrice: number) {
    this.id = id,
      this.name = name,
      this.url = url,
      this.images = images,
      this.purchasePrice = purchasePrice,
      this.salePrice = salePrice
  }
}
