import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Task } from 'src/app/shared/models/task.model';

@Injectable()
export class TaskWebService {
  constructor(private http: HttpClient) { }

  getTasks() {
    return this.http.get<Task[]>('task');
  }
}