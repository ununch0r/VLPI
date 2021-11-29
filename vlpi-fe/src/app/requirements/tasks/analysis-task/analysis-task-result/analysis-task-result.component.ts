import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AnalysisTaskResult } from 'src/app/shared/models/analysis-task-result.model';

@Component({
  selector: 'app-analysis-task-result',
  templateUrl: './analysis-task-result.component.html',
  styleUrls: ['./analysis-task-result.component.scss']
})
export class AnalysisTaskResultComponent implements OnInit {

  taskResult: AnalysisTaskResult;

  constructor(
    private dialogRef: MatDialogRef<AnalysisTaskResultComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {taskResult: AnalysisTaskResult}) {
      this.taskResult = data.taskResult;
     }

  ngOnInit(): void {
  }

  onOk() {
    this.dialogRef.close();
  }

}
