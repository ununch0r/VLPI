import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { map, Observable } from 'rxjs';
import { ExecutionMode } from 'src/app/shared/models/execution-mode.model';
import { Requirement } from 'src/app/shared/models/requirement.model';
import { Task } from 'src/app/shared/models/task.model';
import { PageNameSyncService } from 'src/app/shared/services/page-name.sync-service';
import { ExecutionModeSyncService } from '../../services/execution-mode.sycn-service';
import { TaskSyncService } from '../../services/task.sync-service';

@Component({
  selector: 'app-analysis-task',
  templateUrl: './analysis-task.component.html',
  styleUrls: ['./analysis-task.component.scss']
})
export class AnalysisTaskComponent implements OnInit {
  taskId : number;
  taskObs: Observable<Task>;
  executionMode: ExecutionMode;


  rawRequirements = ['Get tosdk;; lfsl;kl ;asfl ;djjl ;fksdajl ;k ;ljkfsl ;jkfdj ;lkfaskj ;dllkj; work', 'Pick up groceries', 'Pick up groceries', 'Pick up groceries', 'Pick up groceries',  'Pick up groceries', 'Pick up groceries', 'Pick up groceries','Pick up groceries', 'Pick up groceries', 'Go home', 'Fall asleep'];
  correctRequirements : Requirement[] = [];
  wrongRequirements: Requirement[] = [];

  constructor(
    private pageNameService: PageNameSyncService,
    private taskSyncService: TaskSyncService,
    private executionModeSyncService: ExecutionModeSyncService,
    private route: ActivatedRoute
    ) { }

  ngOnInit(): void {
    this.taskSyncService.reloadTasks();
    this.trackTask();
    this.setPageName();
  }

  trackTask(){
    this.route.params
    .subscribe(
      (params: Params) => {
        this.taskId = +params['id'];
        this.initExecution();
      })
  }

  initExecution(){
    this.setupTask();
    this.setupExecutionMode();
  }

  setupTask(){
    this.taskObs = this.taskSyncService.tasksObs.pipe(map(tasks => tasks.find(task => task.id === this.taskId)));
  }

  setupExecutionMode(){
    this.executionMode = this.executionModeSyncService.currentMode;
    console.log(this.executionMode);
  }

  private setPageName(){
    this.pageNameService.setPageName("Requirements analysis task");
  }
  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex,
      );
    }
  }
}
