import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { WritingTaskResult } from 'src/app/shared/models/writing-task-result.model';


@Component({
  selector: 'app-writing-task-result',
  templateUrl: './writing-task-result.component.html',
  styleUrls: ['./writing-task-result.component.scss']
})
export class WritingTaskResultComponent implements OnInit {
  taskResult: WritingTaskResult;

  constructor(
    private dialogRef: MatDialogRef<WritingTaskResultComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {taskResult: WritingTaskResult}) {
      this.taskResult = data.taskResult;
     }

  ngOnInit(): void {
  }

  onOk() {
    this.dialogRef.close();
  }
}
