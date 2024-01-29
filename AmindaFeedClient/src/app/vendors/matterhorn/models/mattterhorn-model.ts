export class MatterhornProduct{
  id:number;
  name:string;
  url:string;
  images:string[]

  constructor(id:number, name:string, url:string, images:string[]){
    this.id=id,
    this.name=name,
    this.url=url,
    this.images=images
  }
}