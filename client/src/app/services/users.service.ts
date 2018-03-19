import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Injectable} from '@angular/core';

import 'rxjs/add/operator/map';
import 'rxjs/Rx';

import {CreateUser} from '../models/create-user';

@Injectable()
export class UsersService {

  baseUrl = 'http://localhost:49791/api/';

  constructor(private http: HttpClient) {}

  getUsers() {
    return this.http.get(this.baseUrl + 'users');
  }

  createUser(user: CreateUser) {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    return this.http.post(this.baseUrl + 'users', user, {headers});
  }
}
