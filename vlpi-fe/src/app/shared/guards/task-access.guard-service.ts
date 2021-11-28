import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { DashboardSyncService } from 'src/app/requirements/services/dashboard.sync-service';

@Injectable({providedIn: 'root'})
export class TaskAccessGuardService implements CanActivate {
  constructor(public dashboardService: DashboardSyncService, public router: Router) {}
  canActivate(): boolean {
    if (!this.dashboardService.isDashboardVisited()) {
      this.router.navigate(['requirements']);
      return false;
    }
    return true;
  }
}