import { Component, OnInit } from '@angular/core';
import { WeatherappService } from '../services/weatherapp.service';
import { WeatherData } from '../models/weatherdata';
import { FormBuilder,FormGroup } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  countries = [];
  cities=[];
  wData:WeatherData={};
  fWeather:FormGroup ;


  constructor(private weatherSvc:WeatherappService
    ,private fb: FormBuilder) { }

  ngOnInit(): void {
    this.weatherSvc.getCountries().subscribe({
      next: (x:any)=>{
        this.countries=x;
      },
      error:(xhr)=>{
        console.error(xhr);
      }
    })

    this.fWeather = this.fb.group({
      weatherDescription:'',
      tempC:0,
      temp:0,
      feelsLikeC:0,
      feelsLike:0,
      tempMin:0,
      tempMax:0,
      windSpeed:0,
      windDeg:0,
      lat:0,
      long:0
    })
  
  }

  OnCountryChange(event:any){
    this.weatherSvc.getCities(event.target.value)
      .subscribe({
        next: (x:any)=>{
          this.cities=x;
        },
        error:(xhr)=>{
          console.error(xhr);
        }
      });
  }

  OnCityChange(event:any){
    this.weatherSvc.getWeather(event.target.value)
    .subscribe({
      next: (x:any)=>{
        this.wData=x;
        console.log(this.wData.main);
        this.BindForm(this.wData);
      },
      error:(xhr)=>{
        console.error(xhr);
      }
    });
  }

  private BindForm(w:WeatherData){
    if(w.weather.length>0){
      this.fWeather.controls["weatherDescription"].setValue(w.weather[0].description);

    }
if(w.main){
  this.fWeather.controls['temp'].setValue(w.main.temp);
  this.fWeather.controls['tempC'].setValue(w.main.tempC);
  this.fWeather.controls['feelsLike'].setValue(w.main.feelsLike);
  this.fWeather.controls['feelsLikeC'].setValue(w.main.feelsLikeC);
  this.fWeather.controls['tempMin'].setValue(w.main.tempMin);
  this.fWeather.controls['tempMax'].setValue(w.main.tempMax);

}
if(w.wind){
  this.fWeather.controls['windSpeed'].setValue(w.wind.speed);
  this.fWeather.controls['windDeg'].setValue(w.wind.deg);

}
if(w.coord){
  this.fWeather.controls['lat'].setValue(w.coord.lat);
  this.fWeather.controls['long'].setValue(w.coord.lon);

}
  }

}
