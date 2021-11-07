import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { ExecutionMode } from 'src/app/shared/models/execution-mode.model';

@Component({
  selector: 'app-choose-difficulty-dialog',
  templateUrl: './choose-difficulty-dialog.component.html',
  styleUrls: ['./choose-difficulty-dialog.component.scss'],
  encapsulation : ViewEncapsulation.None,
})
export class ChooseDifficultyDialogComponent implements OnInit {
  selectedMode : ExecutionMode;

  executionModes: ExecutionMode[] =[
    {id:1, name:"Easy", executionTime:360, wrongRequirementsCount:3},
    {id:2, name:"Difficult", executionTime:360, wrongRequirementsCount:3}
  ]

  constructor(
      private dialogRef: MatDialogRef<ChooseDifficultyDialogComponent>) {

  }

  ngOnInit() {
    this.selectedMode = this.executionModes[0];
  }

  onOk() {
    this.dialogRef.close(this.selectedMode);
  }

  onCancel() {
      this.dialogRef.close();
  }
}
