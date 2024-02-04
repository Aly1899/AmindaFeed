export class MatterhornProduct {
  id: number;
  name: string;
  url: string;
  images: string[];
  purchasePrice: number;
  salePrice: number;
  variants: Variant[]

  constructor(id: number, name: string, url: string, images: string[],
              purchasePrice: number, salePrice: number, variants: Variant[]) {
    this.id = id,
      this.name = name,
      this.url = url,
      this.images = images,
      this.purchasePrice = purchasePrice,
      this.salePrice = salePrice,
      this.variants = variants
  }
}

export class Variant{
  variant_uid:number;
  name:string;
  stock:number
  constructor(variant_uid:number, name:string, stock:number){
    this.variant_uid=variant_uid,
    this.name=name,
    this.stock=stock
  }
}
