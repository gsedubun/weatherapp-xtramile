import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WeatherappService {

  constructor(private httpClient: HttpClient) { }

  getWeather(city:string){
    return this.httpClient.get(environment.base_url+'/home/weather?city='+city);
  }

  getCountries(){
    return this.httpClient.get(environment.base_url+'/home/countries');

  }

  getCities(country:string){
    return this.httpClient.get(environment.base_url+'/home/cities?country='+country);

  }
}
