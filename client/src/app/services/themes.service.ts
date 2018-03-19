import { HttpClient } from '@angular/common/http';

import 'rxjs/add/operator/map';
import 'rxjs/Rx';

import {Injectable} from '@angular/core';

@Injectable()
export class ThemesService {

  baseUrl = 'http://localhost:49791/api/';

  constructor(private http: HttpClient) {
  }

  getThemes() {
    return this.http.get(this.baseUrl + 'themes');
  }
}
