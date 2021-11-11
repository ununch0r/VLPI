import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class TaskWebService {
  constructor(private http: HttpClient) { }

  getConfig() {
    return this.http.get('task');
  }
}