import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { firstValueFrom } from 'rxjs';

interface ApiResponse<T> {
  message: string;
  data: T;
}
export interface ShortenedLink {
  id: number;
  originalUrl: string;
  shortUrl: string;
}
@Injectable({
  providedIn: 'root'
})
export class UrlConverterService {
  private url = environment.apiUrl;
  private headers = new HttpHeaders({
    'Access-Control-Allow-Origin': '*',
    'Content-Type': 'application/json'
  });

  constructor(private http: HttpClient) { }

  getAll() {
    return firstValueFrom(this.http.get<ApiResponse<any>>(`${this.url}/api/LinkShortener`, { headers: this.headers }))
    .then(response => response.data);
  }

  convert(param: string) {
    return firstValueFrom(this.http.post<ApiResponse<ShortenedLink>>(`${this.url}/api/LinkShortener`, { url: param }, { headers: this.headers }))
      .then(response => response.data);
  }
}