import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { first, interval, map, Observable, Subject, takeUntil, takeWhile } from 'rxjs';
import { ExecutionModeSyncService } from 'src/app/requirements/services/execution-mode.sycn-service';
import { SystemStateSyncService } from 'src/app/requirements/services/system-state.sync-service';
import { TaskSyncService } from 'src/app/requirements/services/task.sync-service';
import { AnswerWebService } from 'src/app/requirements/web-services/answer.web-service';
import { shuffle } from 'src/app/shared/helpers/task-helper';
import { AnalysisTaskAnswer } from 'src/app/shared/models/analysis-task-answer.model';
import { AnalysisTaskResult } from 'src/app/shared/models/analysis-task-result.model';
import { CreateWrongRequirement } from 'src/app/shared/models/create-wrong-requirement.model';
import { ExecutionMode } from 'src/app/shared/models/execution-mode.model';
import { Explanation } from 'src/app/shared/models/explanation.model';
import { RequirementType } from 'src/app/shared/models/requirement-type.model';
import { Requirement } from 'src/app/shared/models/requirement.model';
import { Task } from 'src/app/shared/models/task.model';
import { WritingAnswerRequirement } from 'src/app/shared/models/writing-answer-requirement.model';
import { WritingTaskAnswer } from 'src/app/shared/models/writing-task-answer.model';
import { PageNameSyncService } from 'src/app/shared/services/page-name.sync-service';
import { AnalysisTaskResultComponent } from '../analysis-task/analysis-task-result/analysis-task-result.component';

@Component({
  selector: 'app-writing-task',
  templateUrl: './writing-task.component.html',
  styleUrls: ['./writing-task.component.scss']
})
export class WritingTaskComponent implements OnInit {

  writingForm : FormGroup;
  destroySubj = new Subject();
  countDownDestroySubj = new Subject();

  systemName: string = '';
  taskId : number;
  taskObs: Observable<Task>;
  executionMode: ExecutionMode;
  hintsEnabledObs: Observable<boolean>;

  timeLeft: number;
  tipsCount: number;
  isStarted: boolean = false;

  usedTipsCount: number = 0;

  requirementTypesObs: Observable<RequirementType[]>;
  requirementTypes: RequirementType[]
  
  constructor(
    private pageNameService: PageNameSyncService,
    private taskSyncService: TaskSyncService,
    private executionModeSyncService: ExecutionModeSyncService,
    private route: ActivatedRoute,
    private answerWebService: AnswerWebService,
    private dialog: MatDialog,
    private router: Router,
    private systemStateSyncService: SystemStateSyncService
    ) { }

  ngOnInit(): void {
    this.taskSyncService.reloadTasks();
    this.initializeTasks();
    this.trackTaskId();
    this.setPageName();
    this.setUpSystemTracking();
    this.initializeRequirementTypes()
    this.setRequirementTypes();
    this.initForm();
  }

  private initializeTasks():void{
    this.taskSyncService.reloadTasks();
}

  initForm() {
    let task = this.taskSyncService.tasks.find(task => task.id === this.taskId);
    let requirements = new FormArray([]);

    if(task['requirement']){
      for(let requirement of task.requirement){
        requirements.push(
          new FormGroup({
            'description': new FormControl(requirement.description),
            'requirementId' : new FormControl(requirement.id),
            'continuation' : new FormControl(''),
            'typeId': new FormControl(0,  [Validators.pattern(/^[1-9]+[0-9]*$/)])
          })
        );
      }
    }

    this.writingForm = new FormGroup({
      'requirements' : requirements,
    })
  }

  private setRequirementTypes(){
    this.requirementTypesObs = this.taskSyncService.requirementTypesObs;
    this.requirementTypesObs.subscribe(types => this.requirementTypes = types);
  }

  private initializeRequirementTypes(){
    this.taskSyncService.reloadReqruirementTypes();
  }

  setUpSystemTracking(){
    this.hintsEnabledObs = this.systemStateSyncService.hintsEnabledObs.pipe(first());
  }

  ngOnDestroy(): void {
    this.destroySubj.next('');
    this.destroySubj.complete();
  }

  private setPageName(){
    this.pageNameService.setPageName("Writing requirements task");
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
    this.timeLeft = this.executionMode.executionTime;
  }

  trackTask(){
    this.taskObs
    .pipe(takeUntil(this.destroySubj))
    .subscribe(task => {
      this.setupTips(task);
    })
  }


  setupTips(task: Task){
    this.tipsCount = task.taskTip.length;
  }

  onStart(){
    this.isStarted = true;

    interval(1000)
    .pipe(
      takeWhile(val => val < this.executionMode.executionTime),
      takeUntil(this.destroySubj),
      takeUntil(this.countDownDestroySubj))
    .subscribe( _ =>{
      this.timeLeft--;

      if(this.timeLeft === 0){
        this.onComplete();
      }
    })
  }

  onComplete(){
    this.countDownDestroySubj.next('');

    var answer = this.createAnswer();
    this.answerWebService.createWritingTaskAnswer(answer).subscribe(task => {
      //this.showResultOverlay(task);
    });
  }

  showResultOverlay(task: AnalysisTaskResult){
    const dialogRef = this.dialog.open(AnalysisTaskResultComponent,{
      panelClass: 'custom-dialog-container',
      data: {taskResult: task}
    });

    dialogRef.afterClosed()
    .pipe(takeUntil(this.destroySubj))
    .subscribe(_ => {
            this.router.navigate(['requirements']);
          }
    );
  }

  createAnswer() : WritingTaskAnswer{
    let formRequirements = this.writingForm.value.requirements;
    let requirements: WritingAnswerRequirement[] = formRequirements.map(requirement =>({
      requirementId: requirement.requirementId, continuation: requirement.continuation, typeId: requirement.typeId
    }as WritingAnswerRequirement ));

    return {
      systemName: this.systemName,
      taskId: this.taskId,
      timeSpent: this.executionMode.executionTime - this.timeLeft,
      usedTipsCount: this.usedTipsCount,
      requirements: requirements
    }
  }

  showHint(){
    this.usedTipsCount++;
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
