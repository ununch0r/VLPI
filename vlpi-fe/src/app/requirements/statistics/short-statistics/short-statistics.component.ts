import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
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

  userId: number | null;
  constructor(
    private dialogRef: MatDialogRef<ShortStatisticsComponent>,
    private statisticSyncService: StatisticSyncService,
    @Inject(MAT_DIALOG_DATA) public data: {userId: number | null}) {
      this.userId = data.userId;
     }

  ngOnInit(): void {
    if(!!this.userId){
      this.statisticSyncService.reloadShortStatisticByUserId(this.userId);
    }else{
      this.initializeShortStatistic();
    }
    this.loadShortStatistic();
  }

  initializeShortStatistic(){
    this.statisticSyncService.reloadShortStatistic();
  }

  loadShortStatistic(){
    this.userShortStatisticObs = this.statisticSyncService.userShortStatisticObs;
  }

  onCertificate(){
    this.dialogRef.close();
  }

  onCancel(){
    this.dialogRef.close();
  }

}
