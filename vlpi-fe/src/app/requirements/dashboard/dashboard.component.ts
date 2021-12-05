import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { first, Observable, Subject, takeUntil, tap } from 'rxjs';

import { SimpleTask } from 'src/app/shared/models/simple-task.model';
import { Tile } from 'src/app/shared/models/tile.model';
import { PageNameSyncService } from 'src/app/shared/services/page-name.sync-service';
import { UserSyncService } from 'src/app/shared/services/user.sync.service';
import { DashboardSyncService } from '../services/dashboard.sync-service';
import { ExecutionModeSyncService } from '../services/execution-mode.sycn-service';
import { SystemStateSyncService } from '../services/system-state.sync-service';
import { TaskSyncService } from '../services/task.sync-service';
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
  hintsEnabledObs: Observable<boolean>;

  constructor(
    private router: Router,
    private dialog: MatDialog,
    private taskSyncService: TaskSyncService,
    private pageNameService: PageNameSyncService,
    private executionModeSyncService: ExecutionModeSyncService,
    private dashboardSyncService: DashboardSyncService,
    private systemStateSyncService: SystemStateSyncService,
    private userService: UserSyncService
    ) { }

  ngOnInit(): void {
    this.initializeTasks();
    this.loadTasks();
    this.setPageName();
    this.dashboardSyncService.setDashboardVisitedStatus(true);
    this.setUpSystemTracking();
  }

  setUpSystemTracking(){
    this.hintsEnabledObs = this.systemStateSyncService.hintsEnabledObs.pipe(first());
  }

  onHintsStateChange(values:any){
    this.systemStateSyncService.setHintsEnabledValue(values.currentTarget.checked);
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

  tiles = [
    {header: 'Statistics', text: 'Check your statistics', navigation: '/statistics', isVisible: true},
    {header: 'Administration', text: 'Manage tasks and users', navigation: '/administration', isVisible: this.userService.isUserAdmin()}
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
            this.executionModeSyncService.setCurrentExecutionMode(result.id);
            if(taskType == 'Requirement analysis'){
              this.router.navigate(['analysis-task', taskId]);
            }else{
              this.router.navigate(['writing-task', taskId]);
            }
        }
      });
    }
}
