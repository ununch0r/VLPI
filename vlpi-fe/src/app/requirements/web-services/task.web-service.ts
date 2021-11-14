import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Task } from 'src/app/shared/models/task.model';

const prefix: string = 'task';

@Injectable()
export class TaskWebService {
  constructor(private http: HttpClient) { }

  getTasks() {
    return this.http.get<Task[]>(prefix);
  }

  deleteTask(taskId: number){
    return this.http.delete(prefix + '/' + taskId);
  }
}