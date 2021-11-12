import { Injectable } from '@angular/core';
import { map, Observable, Subject } from 'rxjs';
import { SimpleTask } from 'src/app/shared/models/simple-task.model';
import { Task } from 'src/app/shared/models/task.model';

@Injectable()
export class TaskSyncService {
  
  private tasksSubj = new Subject<Task[]>();

  tasksObs = this.tasksSubj.asObservable();
  simpleTaskObs : Observable<SimpleTask[]>;

  constructor() {
   this.setupSimpleTasks();
  }

  private setupSimpleTasks(){
    this.simpleTaskObs = this.tasksObs.pipe(map(tasks =>tasks.map(task => {
      return {id: task.id, order: task.order, type: task.type.name} as SimpleTask })))
  }

  reloadTasks(tasks: Task[])
  {
    this.tasksSubj.next(tasks);
  }
  
}