import { Injectable } from '@angular/core';
import { map, Observable, Subject, tap } from 'rxjs';
import { AnalysisTask } from 'src/app/shared/models/analysis-task.model';
import { RequirementType } from 'src/app/shared/models/requirement-type.model';
import { SimpleTask } from 'src/app/shared/models/simple-task.model';
import { Task } from 'src/app/shared/models/task.model';
import { WritingTask } from 'src/app/shared/models/writing-task.model';
import { TaskWebService } from '../web-services/task.web-service';
import { UtilsWebService } from '../web-services/utils.web-service';

@Injectable()
export class TaskSyncService {
  
  private tasksSubj = new Subject<Task[]>();
  private requirementTypesSubj = new Subject<RequirementType[]>();

  tasks : Task[];
  tasksObs = this.tasksSubj.asObservable();
  simpleTaskObs : Observable<SimpleTask[]>;

  requirementTypes : RequirementType[];
  requirementTypesObs = this.requirementTypesSubj.asObservable();

  constructor(
    private taskWebService: TaskWebService,
    private utilsWebService: UtilsWebService
    ) {
   this.setupSimpleTasks();
   this.setupTasks();
   this.setupRequirementTypes();
  }

  private setupTasks(){
    this.tasksObs.subscribe(tasks => this.tasks = tasks);
  }

  private setupRequirementTypes(){
    this.requirementTypesObs.subscribe(types => this.requirementTypes = types);
  }

  private setupSimpleTasks(){
    this.simpleTaskObs = this.tasksObs.pipe(map(tasks =>tasks.map(task => {
      return {id: task.id, order: task.order, type: task.type.name} as SimpleTask })))
  }

  reloadTasks()
  {
    this.taskWebService.getTasks().subscribe(tasks => 
      {
        this.tasksSubj.next(tasks)
      });
  }

  reloadReqruirementTypes()
  {
    this.utilsWebService.getRequirementTypes().subscribe(types => 
      {
        this.requirementTypesSubj.next(types)
      });
  }

  deleteTask(taskId: number){
    this.taskWebService.deleteTask(taskId).subscribe(result => 
      this.reloadTasks());
  }

  createAnalysisTask(task: AnalysisTask){
    this.taskWebService.createAnalysisTask(task).subscribe(result => 
      this.reloadTasks());
  }

  createWritingTask(task: WritingTask){
    this.taskWebService.createWritingTask(task).subscribe(result => 
      this.reloadTasks());
  }
  
}