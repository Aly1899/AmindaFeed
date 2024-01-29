import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import { environment } from '../../../../environments/environment.development';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MatterhornService {

  constructor(private readonly httpClient: HttpClient) {}

  getProductByCategory(category: number):Observable<any[]>{
    return this.httpClient.get<any[]>(`${environment.backendUri}/Product/GetMatterhornProductsByCategory?category=${category}`)
  }
}
