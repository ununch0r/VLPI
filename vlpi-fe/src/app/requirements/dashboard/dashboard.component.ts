import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';

import { SimpleTask } from 'src/app/shared/models/simple-task.model';
import { Tile } from 'src/app/shared/models/tile.model';
import { PageNameSyncService } from 'src/app/shared/services/page-name.sync-service';
import { TaskSyncService } from '../services/task.sync-service';
import { TaskWebService } from '../web-services/task.web-service';
import { ChooseDifficultyDialogComponent } from './choose-difficulty-dialog/choose-difficulty-dialog.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  checked = true;
  tasksObs: Observable<SimpleTask[]>;

  constructor(
    private router: Router,
    private dialog: MatDialog,
    private taskSyncService: TaskSyncService,
    private pageNameService: PageNameSyncService
    ) { }

  ngOnInit(): void {
    this.initializeTasks();
    this.loadTasks();
    this.setPageName();
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

    chooseDifficulty(id : number){
      const dialogRef = this.dialog.open(ChooseDifficultyDialogComponent);

      dialogRef.afterClosed().subscribe(result => 
        console.log(result)
        );
    }
}
