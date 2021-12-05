import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Task } from 'src/app/shared/models/task.model';
import { AnalysisTask } from 'src/app/shared/models/analysis-task.model';
import { WritingTask } from 'src/app/shared/models/writing-task.model';
import { CreateTask } from 'src/app/shared/models/create-task.model';

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

  createAnalysisTask(task : AnalysisTask){
    return this.http.post(prefix+ '/' + 'analysis', task)
  }

  createWritingTask(task : WritingTask){
    return this.http.post(prefix+ '/' + 'writing', task)
  }

  updateTask(id: number, task : CreateTask){
    return this.http.put(prefix+ '/' + id, task)
  }
}