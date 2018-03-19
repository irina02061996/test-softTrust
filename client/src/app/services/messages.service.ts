import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Injectable} from '@angular/core';

import 'rxjs/add/operator/map';
import 'rxjs/Rx';

import {CreateMessage} from '../models/create-message';


@Injectable()
export class MessagesService {

  baseUrl = 'http://localhost:49791/api/';

  constructor(private http: HttpClient) {}

  getMessages() {
    return this.http.get(this.baseUrl + 'messages');
  }

  createMessage(message: CreateMessage) {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    return this.http.post(this.baseUrl + 'messages', message, {headers});
  }
}
