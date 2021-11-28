import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { interval, map, Observable, Subject, takeUntil, takeWhile } from 'rxjs';
import { shuffle } from 'src/app/shared/helpers/task-helper';
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
export class AnalysisTaskComponent implements OnInit, OnDestroy {
  destroySubj = new Subject();

  taskId : number;
  taskObs: Observable<Task>;
  executionMode: ExecutionMode;

  inputRequirements: Requirement[] = [];
  executionTime: number;
  tipsCount: number;
  isStarted: boolean = false;

  correctRequirements: Requirement[] = [];
  wrongRequirements: Requirement[] = [];
  usedTipsCount: number = 0;

  constructor(
    private pageNameService: PageNameSyncService,
    private taskSyncService: TaskSyncService,
    private executionModeSyncService: ExecutionModeSyncService,
    private route: ActivatedRoute
    ) { }

  ngOnInit(): void {
    this.taskSyncService.reloadTasks();
    this.trackTaskId();
    this.setPageName();
  }

  ngOnDestroy(): void {
    this.destroySubj.next('');
    this.destroySubj.complete();
  }

  private setPageName(){
    this.pageNameService.setPageName("Requirements analysis task");
  }

  trackTaskId(){
    this.route.params
    .pipe(takeUntil(this.destroySubj))
    .subscribe(
      (params: Params) => {
        this.taskId = +params['id'];
        this.initExecution();
      })
  }

  initExecution(){
    this.setupTask();
    this.setupExecutionMode();
    this.trackTask();
  }

  setupTask(){
    this.taskObs = this.taskSyncService.tasksObs.pipe(map(tasks => tasks.find(task => task.id === this.taskId)));
  }

  setupExecutionMode(){
    this.executionMode = this.executionModeSyncService.currentMode;
    this.executionTime = this.executionMode.executionTime;
  }

  trackTask(){
    this.taskObs
    .pipe(takeUntil(this.destroySubj))
    .subscribe(task => {
      this.setupInputRequirements(task);
      this.setupTips(task);
    })
  }

  setupInputRequirements(task: Task){
    let requirements = shuffle(task.requirement);

    let wrongRequirements = requirements.filter(req => !req.isCorrect).slice(0, this.executionMode.wrongRequirementsCount);
    let correctRequirements = requirements.filter(req => req.isCorrect);

    let combinedArray = [...wrongRequirements, ...correctRequirements];
    this.inputRequirements = shuffle(combinedArray);
  }

  setupTips(task: Task){
    this.tipsCount = task.taskTip.length;
  }

  onStart(){
    this.isStarted = true;

    interval(1000)
    .pipe(
      takeWhile(val => val < this.executionMode.executionTime),
      takeUntil(this.destroySubj))
    .subscribe( _ =>{
      this.executionTime--;

      if(this.executionTime === 0){
        this.onComplete();
      }
    })
  }

  onComplete(){
    console.log('completed');
  }

  showHint(){
    this.usedTipsCount++;
    console.log(this.usedTipsCount, this.tipsCount);
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
