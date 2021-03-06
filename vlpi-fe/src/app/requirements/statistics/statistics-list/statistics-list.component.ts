import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PageNameSyncService } from 'src/app/shared/services/page-name.sync-service';
import { TaskSyncService } from '../../services/task.sync-service';
import { Observable, takeUntil } from 'rxjs';
import { Task } from 'src/app/shared/models/task.model';
import { StatisticSyncService } from '../../services/statistic.sync-service';
import { UserTaskStatistic } from 'src/app/shared/models/user-task-statistic.model';
import { MatDialog } from '@angular/material/dialog';
import { ShortStatisticsComponent } from '../short-statistics/short-statistics.component';
import { StatisticInfoComponent } from '../statistic-info/statistic-info.component';

@Component({
  selector: 'app-statistics-list',
  templateUrl: './statistics-list.component.html',
  styleUrls: ['./statistics-list.component.scss']
})
export class StatisticsListComponent implements OnInit {

  statisticObs: Observable<UserTaskStatistic[]>;

  constructor(
    private statisticSyncService: StatisticSyncService,
    private pageNameService: PageNameSyncService,
    private dialog: MatDialog,
    ) { }

  ngOnInit(): void {
    this.initializeTasks();
    this.loadTasks();
    this.setPageName();
  }

  private setPageName(){
    this.pageNameService.setPageName("User statistic");
  }

  private initializeTasks():void{
      this.statisticSyncService.reloadUserStatistic();
  }

  private loadTasks(): void{
    this.statisticObs = this.statisticSyncService.userStatisticObs;
  }

  info(answerId: number){
    let userAnswer = this.statisticSyncService.userStatistic.find(stat => stat.answerId === answerId);
    const dialogRef = this.dialog.open(StatisticInfoComponent, {
      panelClass: 'statistic-info-dialog-container',
      data: {statistic: userAnswer}
    });
  }

  onShortStat(){
    const dialogRef = this.dialog.open(ShortStatisticsComponent, {
      panelClass: 'short-statistic-dialog-container',
      data: {userId: null}
    });
  }

}
