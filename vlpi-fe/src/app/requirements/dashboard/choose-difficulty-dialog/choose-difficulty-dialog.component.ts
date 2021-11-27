import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { ExecutionMode } from 'src/app/shared/models/execution-mode.model';
import { ExecutionModeSyncService } from '../../services/execution-mode.sycn-service';

@Component({
  selector: 'app-choose-difficulty-dialog',
  templateUrl: './choose-difficulty-dialog.component.html',
  styleUrls: ['./choose-difficulty-dialog.component.scss'],
  encapsulation : ViewEncapsulation.None,
})
export class ChooseDifficultyDialogComponent implements OnInit {
  selectedMode : ExecutionMode;

  executionModes: ExecutionMode[]

  constructor(
      private dialogRef: MatDialogRef<ChooseDifficultyDialogComponent>,
      private executionModeSyncService: ExecutionModeSyncService) {

  }

  ngOnInit() {
    this.executionModes = this.executionModeSyncService.executionModes;
    this.selectedMode = this.executionModes[0];
  }


  onOk() {
    this.dialogRef.close(this.selectedMode);
  }

  onCancel() {
      this.dialogRef.close();
  }
}
