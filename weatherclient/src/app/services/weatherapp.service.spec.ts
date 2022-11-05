import { TestBed } from '@angular/core/testing';

import { WeatherappService } from './weatherapp.service';

describe('WeatherappService', () => {
  let service: WeatherappService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WeatherappService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
