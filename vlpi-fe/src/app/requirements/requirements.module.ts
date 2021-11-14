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
import { MatTabsModule } from '@angular/material/tabs';
import { MatIconModule } from '@angular/material/icon';

import { ChooseDifficultyDialogComponent } from './dashboard/choose-difficulty-dialog/choose-difficulty-dialog.component';
import { TaskWebService } from './web-services/task.web-service';
import { TaskSyncService } from './services/task.sync-service';
import { ChooseManagementComponent } from './administration/choose-management/choose-management.component';
import { RouterModule, Routes } from '@angular/router';
import { UserManagementComponent } from './administration/user-management/user-management.component';
import { TaskManagementComponent } from './administration/task-management/task-management.component';
import { EditTaskComponent } from './administration/task-management/edit-task/edit-task.component';

const routes: Routes = [
  { path: 'administration', component: ChooseManagementComponent},
  { path: 'user', component: UserManagementComponent },
  { path: 'task', component: TaskManagementComponent },
  { path: 'edit-task', component: EditTaskComponent },
  { path: 'edit-task/:id', component: EditTaskComponent },
];

@NgModule({
  declarations: [
    DashboardComponent,
    ChooseDifficultyDialogComponent,
    ChooseManagementComponent,
    UserManagementComponent,
    TaskManagementComponent,
    EditTaskComponent,
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
    MatRadioModule,
    MatTabsModule,
    MatIconModule,
    RouterModule.forRoot(routes)
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
