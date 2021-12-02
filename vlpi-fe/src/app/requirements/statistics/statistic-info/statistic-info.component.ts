import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserTaskStatistic } from 'src/app/shared/models/user-task-statistic.model';

@Component({
  selector: 'app-statistic-info',
  templateUrl: './statistic-info.component.html',
  styleUrls: ['./statistic-info.component.scss']
})
export class StatisticInfoComponent implements OnInit {
  statistic: UserTaskStatistic;

  constructor(
    private dialogRef: MatDialogRef<StatisticInfoComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {statistic: UserTaskStatistic}) {
      this.statistic = data.statistic;
     }

  ngOnInit(): void {
  }

  onOk(){
    this.dialogRef.close();
  }
}
