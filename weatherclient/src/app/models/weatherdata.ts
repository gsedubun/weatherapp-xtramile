
export interface WeatherData {
    base?:       string;
    visibility?: number;
    dt?:         number;
    timezone?:   number;
    id?:         number;
    name?:       string;
    cod?:        number;

    coord?:      Coord;
    weather?:    Weather[];
    main?:       Main;

    wind?:       Wind;
    sys?:        Sys;
    clouds?:     Clouds;
}

export interface Clouds {
    all: number;
}

export interface Coord {
    lon: number;
    lat: number;
}

export interface Main {
    tempC:       number;
    feelsLikeC: number;
    temp:       number;
    feelsLike: number;
    tempMin:   number;
    tempMax:   number;
    pressure:   number;
    humidity:   number;
    sea_level:  number;
    grnd_level: number;
}

export interface Sys {
    country: string;
    sunrise: number;
    sunset:  number;
}

export interface Weather {
    id:          number;
    main:        string;
    description: string;
    icon:        string;
}

export interface Wind {
    speed: number;
    deg:   number;
    gust:  number;
}
