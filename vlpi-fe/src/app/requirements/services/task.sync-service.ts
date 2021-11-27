import { Injectable } from '@angular/core';
import { map, Observable, Subject, tap } from 'rxjs';
import { AnalysisTask } from 'src/app/shared/models/analysis-task.model';
import { SimpleTask } from 'src/app/shared/models/simple-task.model';
import { Task } from 'src/app/shared/models/task.model';
import { TaskWebService } from '../web-services/task.web-service';

@Injectable()
export class TaskSyncService {
  
  private tasksSubj = new Subject<Task[]>();

  tasks : Task[];
  tasksObs = this.tasksSubj.asObservable();
  simpleTaskObs : Observable<SimpleTask[]>;

  constructor(private taskWebService: TaskWebService) {
   this.setupSimpleTasks();
   this.setupTasks();
  }

  private setupTasks(){
    this.tasksObs.subscribe(tasks => this.tasks = tasks);
  }

  private setupSimpleTasks(){
    this.simpleTaskObs = this.tasksObs.pipe(map(tasks =>tasks.map(task => {
      return {id: task.id, order: task.order, type: task.type.name} as SimpleTask })))
  }

  reloadTasks()
  {
    this.taskWebService.getTasks().subscribe(tasks => 
      this.tasksSubj.next(tasks));
  }

  deleteTask(taskId: number){
    this.taskWebService.deleteTask(taskId).subscribe(result => 
      this.reloadTasks());
  }

  createAnalysisTask(task: AnalysisTask){
    this.taskWebService.createAnalysisTask(task).subscribe(result => 
      this.reloadTasks());
  }
  
}