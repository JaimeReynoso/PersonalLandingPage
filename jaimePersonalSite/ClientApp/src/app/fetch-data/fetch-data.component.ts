import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];
  public topNews: News[];
  public weather: Weather[];
  public loc: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<News[]>(baseUrl + 'api/News/GetTopNews').subscribe(result => {
      console.log(JSON.stringify(result));
      this.topNews = result;
    }, error => console.error(error));
    navigator.geolocation.getCurrentPosition(resp => {

      this.loc = 'latitude: ' + resp.coords.latitude + ' longitude ' + resp.coords.longitude;
      http.get<Weather[]>(baseUrl + 'api/Weather/GetWeatherForecast/' + resp.coords.latitude + '/' + resp.coords.longitude).subscribe(result => {
        this.weather = result;
      }, error => console.error(error));
    });

  }
}

interface Weather {
  name: string;
  temperature: number;
  icon: string;
  shortForecast: string;
  temperatureUnit: string;
  startTime: Date;
  endTime: Date;
}
interface Thumbnail {
  url: string;
  caption: string;
  height: number;
  width: number;
  type: string;
}
interface News {
  section: string;
  title: string;
  abstract: string;
  url: string;
  multimedia: Thumbnail[];
}


interface WeatherForecast {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
