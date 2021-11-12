import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatRadioModule } from '@angular/material/radio';

import { ChooseDifficultyDialogComponent } from './dashboard/choose-difficulty-dialog/choose-difficulty-dialog.component';
import { TaskWebService } from './web-services/task.web-service';
import { TaskSyncService } from './services/task.sync-service';

@NgModule({
  declarations: [
    DashboardComponent,
    ChooseDifficultyDialogComponent,
  ],
  imports: [
    CommonModule,
    MatGridListModule,
    MatCheckboxModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatListModule,
    MatCardModule,
    MatDialogModule,
    MatFormFieldModule,
    MatRadioModule
  ],
  providers: [
    TaskWebService,
    TaskSyncService
  ],
  entryComponents:[
    ChooseDifficultyDialogComponent
  ]
})
export class RequirementsModule { }
