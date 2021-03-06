import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { first, interval, map, Observable, Subject, takeUntil, takeWhile } from 'rxjs';
import { shuffle } from 'src/app/shared/helpers/task-helper';
import { AnalysisTaskAnswer } from 'src/app/shared/models/analysis-task-answer.model';
import { AnalysisTaskResult } from 'src/app/shared/models/analysis-task-result.model';
import { CreateWrongRequirement } from 'src/app/shared/models/create-wrong-requirement.model';
import { ExecutionMode } from 'src/app/shared/models/execution-mode.model';
import { Explanation } from 'src/app/shared/models/explanation.model';
import { Requirement } from 'src/app/shared/models/requirement.model';
import { Task } from 'src/app/shared/models/task.model';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { PageNameSyncService } from 'src/app/shared/services/page-name.sync-service';
import { ExecutionModeSyncService } from '../../services/execution-mode.sycn-service';
import { SystemStateSyncService } from '../../services/system-state.sync-service';
import { TaskSyncService } from '../../services/task.sync-service';
import { AnswerWebService } from '../../web-services/answer.web-service';
import { AnalysisTaskResultComponent } from './analysis-task-result/analysis-task-result.component';

@Component({
  selector: 'app-analysis-task',
  templateUrl: './analysis-task.component.html',
  styleUrls: ['./analysis-task.component.scss']
})
export class AnalysisTaskComponent implements OnInit, OnDestroy {
  destroySubj = new Subject();
  countDownDestroySubj = new Subject();

  taskId : number;
  taskObs: Observable<Task>;
  executionMode: ExecutionMode;
  hintsEnabledObs: Observable<boolean>;

  inputRequirements: Requirement[] = [];
  explanations: Explanation[] = [];
  timeLeft: number;
  tipsCount: number;
  isStarted: boolean = false;
  task: Task;

  correctRequirements: Requirement[] = [];
  wrongRequirements: Requirement[] = [];
  usedTipsCount: number = 0;

  wrongRequirementsMock: Requirement[] = [
    {id: 1, description: 'assfsdfa sfd sd sdf sd fsd fsdf sdf sdf dsf sdf f dfs  asf asd asfdasdf', isCorrect: false, explanationId: null},
    {id: 2, description: 'assfsdfa asf asd asfdasdf dsfa', isCorrect: false, explanationId: null},
    {id: 3, description: 'assfsdfa sadf asf asd asfdasdf dsfa', isCorrect: false, explanationId: null},
  ]
  explanationsMock: Explanation[] = [{id:1, explanation:'safsdf sdaf sadf kljasdfkl jksd jkfasdkj fkjlasdklj faksljd fkljasdlkj fjklsd '}, {id:2, explanation:'safsdf'}]

  constructor(
    private pageNameService: PageNameSyncService,
    private taskSyncService: TaskSyncService,
    private executionModeSyncService: ExecutionModeSyncService,
    private route: ActivatedRoute,
    private answerWebService: AnswerWebService,
    private dialog: MatDialog,
    private router: Router,
    private systemStateSyncService: SystemStateSyncService,
    private notificationService : NotificationService
    ) { }

  ngOnInit(): void {
    this.taskSyncService.reloadTasks();
    this.trackTaskId();
    this.setPageName();
    this.setUpSystemTracking();
  }

  setUpSystemTracking(){
    this.hintsEnabledObs = this.systemStateSyncService.hintsEnabledObs.pipe(first());
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
        this.task = this.taskSyncService.tasks.find(task => task.id === this.taskId);
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
      this.setupInputRequirements(task);
      this.setupTips(task);
      this.setupExplanations(task);
    })
  }

  setupExplanations(task: Task){
    this.explanations = task.explanation;
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
    this.answerWebService.createAnalysisTaskAnswer(answer).subscribe(task => {
      this.showResultOverlay(task);
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

  createAnswer() : AnalysisTaskAnswer{
    let correctRequirementIds = this.correctRequirements.map(req => req.id);
    let wrongRequirements = this.wrongRequirements.map(req => 
      ({requirementId: req.id, explanationId: req.explanationId} as CreateWrongRequirement));

    return {
      taskId: this.taskId,
      correctRequirements: correctRequirementIds,
      wrongRequirements: wrongRequirements,
      timeSpent: this.executionMode.executionTime - this.timeLeft,
      usedTipsCount: this.usedTipsCount,
      expectedWrongRequirementsCount: this.executionMode.wrongRequirementsCount,
    }
  }

  showHint(){
    this.notificationService.showInfo('', this.task.taskTip[this.usedTipsCount].description);
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
