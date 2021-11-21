import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user.model';

const prefix: string = 'auth';

@Injectable()
export class UserWebService {
  constructor(private http: HttpClient) { }

  getCurrentUser() {
    return this.http.get<User>(prefix + '/' + 'user');
  }
}