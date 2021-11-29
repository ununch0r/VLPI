import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { AnalysisTaskResult } from 'src/app/shared/models/analysis-task-result.model';

@Component({
  selector: 'app-analysis-task-result',
  templateUrl: './analysis-task-result.component.html',
  styleUrls: ['./analysis-task-result.component.scss']
})
export class AnalysisTaskResultComponent implements OnInit {

  taskResult: AnalysisTaskResult= {
    score: 100,
    timeSpent: 200,
    correctRequirements: ['requirement 1 requirement 1 requirement 1', 'requirement 2 requirement 2 requirement 2 requirement 2', 'requirement 3 requirement 3 requirement 3 requirement 3 requirement 3'],
    wrongRequirements: [{description: 'requirement 1 requirement 1 requirement 1', explanation: 'explanation 1 explanation 1 explanation 1', isCorrect: null }, {description: 'requirement 1 requirement 1 requirement 1', explanation: 'explanation 1 explanation 1 explanation 1', isCorrect: null }, {description: 'requirement 1 requirement 1 requirement 1', explanation: 'explanation 1 explanation 1 explanation 1', isCorrect: null }]
  }

  constructor(
    private dialogRef: MatDialogRef<AnalysisTaskResultComponent>,
      ) { }

  ngOnInit(): void {
  }

  onOk() {
    this.dialogRef.close();
  }

}
