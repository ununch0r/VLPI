import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { ShortStatistic } from 'src/app/shared/models/short-statistic.model';
import { StatisticSyncService } from '../../services/statistic.sync-service';

@Component({
  selector: 'app-short-statistics',
  templateUrl: './short-statistics.component.html',
  styleUrls: ['./short-statistics.component.scss']
})
export class ShortStatisticsComponent implements OnInit {

  userShortStatisticObs: Observable<ShortStatistic[]>;

  constructor(
    private dialogRef: MatDialogRef<ShortStatisticsComponent>,
    private statisticSyncService: StatisticSyncService
  ) { }

  ngOnInit(): void {
    this.initializeShortStatistic();
    this.loadShortStatistic();
  }

  initializeShortStatistic(){
    this.statisticSyncService.reloadShortStatistic();
  }

  loadShortStatistic(){
    this.userShortStatisticObs = this.statisticSyncService.userShortStatisticObs;
  }

  onCertificate(){

  }

  onCancel(){
    this.dialogRef.close();
  }

}
