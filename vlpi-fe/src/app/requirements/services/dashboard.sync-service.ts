import { Injectable } from '@angular/core';

@Injectable()
export class DashboardSyncService {
  private dashboardVisited = false;

  constructor() {
  }

  setDashboardVisitedStatus(isDashboardVisited: boolean){
      this.dashboardVisited = isDashboardVisited;
  }

  isDashboardVisited(): boolean{
    return this.dashboardVisited;
  }
}