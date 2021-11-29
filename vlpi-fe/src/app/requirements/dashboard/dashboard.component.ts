import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Observable, Subject, takeUntil, tap } from 'rxjs';

import { SimpleTask } from 'src/app/shared/models/simple-task.model';
import { Tile } from 'src/app/shared/models/tile.model';
import { PageNameSyncService } from 'src/app/shared/services/page-name.sync-service';
import { DashboardSyncService } from '../services/dashboard.sync-service';
import { ExecutionModeSyncService } from '../services/execution-mode.sycn-service';
import { TaskSyncService } from '../services/task.sync-service';
import { AnalysisTaskResultComponent } from '../tasks/analysis-task/analysis-task-result/analysis-task-result.component';
import { TaskWebService } from '../web-services/task.web-service';
import { ChooseDifficultyDialogComponent } from './choose-difficulty-dialog/choose-difficulty-dialog.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit, OnDestroy {
  destroySubj = new Subject();

  checked = true;
  tasksObs: Observable<SimpleTask[]>;

  constructor(
    private router: Router,
    private dialog: MatDialog,
    private taskSyncService: TaskSyncService,
    private pageNameService: PageNameSyncService,
    private executionModeSyncService: ExecutionModeSyncService,
    private dashboardSyncService: DashboardSyncService
    ) { }

  ngOnInit(): void {
    this.initializeTasks();
    this.loadTasks();
    this.setPageName();
    this.dashboardSyncService.setDashboardVisitedStatus(true);
  }

  ngOnDestroy(): void {
    this.destroySubj.next('');
    this.destroySubj.complete();
  }

  private setPageName(){
    this.pageNameService.setPageName("Requirements analysis module");
  }

  private initializeTasks():void{
    this.taskSyncService.reloadTasks();
  }

  private loadTasks(): void{
    this.tasksObs = this.taskSyncService.simpleTaskObs;
  }

  tiles: Tile[] = [
    {header: 'Statistics', text: 'Check your statistics', navigation: '/statistics'},
    {header: 'Administration', text: 'Manage tasks and users', navigation: '/administration'}
    ];

    goToSubModule(navigationPath: string){
      this.router.navigate([navigationPath]);
    }

    chooseDifficulty(taskId: number, taskType: string){
      const dialogRef = this.dialog.open(ChooseDifficultyDialogComponent, {panelClass: 'choose-difficulty-dialog-container'});

      dialogRef.afterClosed()
      .pipe(takeUntil(this.destroySubj))
      .subscribe(result => {
          if(!!result){
            if(taskType == 'Requirement analysis'){
              this.executionModeSyncService.setCurrentExecutionMode(result.id);
              this.router.navigate(['analysis-task', taskId]);
            }
        }
      });
    }
}
