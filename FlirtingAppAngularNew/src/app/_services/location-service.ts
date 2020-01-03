import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocationService {
  getCurrentPosition(): Promise<Position> {
    return new Promise((resolve, reject) => {

      navigator.geolocation.getCurrentPosition((position: Position) => {

          resolve(position);
        },
        err => {
          reject(err);
        });
    });
  }
}
