import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { User } from 'src/app/shared/models/user.model';
import { PageNameSyncService } from 'src/app/shared/services/page-name.sync-service';
import { UserSyncService } from 'src/app/shared/services/user.sync.service';
import { ShortStatisticsComponent } from '../../statistics/short-statistics/short-statistics.component';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.scss']
})
export class UserManagementComponent implements OnInit {
  usersObs: Observable<User[]>;

  constructor(
    private router: Router,
    private pageNameService: PageNameSyncService,
    private userSyncService: UserSyncService,
    private dialog: MatDialog,
    ) { }

  ngOnInit(): void {
    this.initializeUsers();
    this.loadUsers();
    this.setPageName();
  }

  private setPageName(){
    this.pageNameService.setPageName("User management");
  }

  private initializeUsers():void{
      this.userSyncService.reloadUsers();
  }

  private loadUsers(): void{
    this.usersObs = this.userSyncService.usersObs;
  }

  updateRole(event: any, userId){
    console.log(event.target.checked, userId)
  }

  showStatistic(userId: number){
    const dialogRef = this.dialog.open(ShortStatisticsComponent, {
      panelClass: 'short-statistic-dialog-container',
      data: {userId: userId}
    });
  }
}
