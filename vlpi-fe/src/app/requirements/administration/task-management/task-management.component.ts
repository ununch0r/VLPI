import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { Task } from 'src/app/shared/models/task.model';
import { PageNameSyncService } from 'src/app/shared/services/page-name.sync-service';
import { TaskSyncService } from '../../services/task.sync-service';
import { TaskWebService } from '../../web-services/task.web-service';

@Component({
  selector: 'app-task-management',
  templateUrl: './task-management.component.html',
  styleUrls: ['./task-management.component.scss']
})
export class TaskManagementComponent implements OnInit {

  tasksObs: Observable<Task[]>;

  constructor(
    private router: Router,
    private taskSyncService: TaskSyncService,
    private pageNameService: PageNameSyncService
    ) { }

  ngOnInit(): void {
    this.initializeTasks();
    this.loadTasks();
    this.setPageName();
  }

  private setPageName(){
    this.pageNameService.setPageName("Task management");
  }

  private initializeTasks():void{
      this.taskSyncService.reloadTasks();
  }

  private loadTasks(): void{
    this.tasksObs = this.taskSyncService.tasksObs;
  }

  deleteTask(taskId: number){
    this.taskSyncService.deleteTask(taskId);
  }

  editTask(taskId: number){
    this.router.navigate(['edit-task', taskId]);
  }

}
