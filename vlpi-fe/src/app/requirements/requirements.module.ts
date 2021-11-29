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
import {DragDropModule} from '@angular/cdk/drag-drop';

import { ChooseDifficultyDialogComponent } from './dashboard/choose-difficulty-dialog/choose-difficulty-dialog.component';
import { TaskWebService } from './web-services/task.web-service';
import { TaskSyncService } from './services/task.sync-service';
import { ChooseManagementComponent } from './administration/choose-management/choose-management.component';
import { RouterModule, Routes } from '@angular/router';
import { UserManagementComponent } from './administration/user-management/user-management.component';
import { TaskManagementComponent } from './administration/task-management/task-management.component';
import { EditTaskComponent } from './administration/task-management/edit-task/edit-task.component';
import { EditWritingComponent } from './administration/task-management/edit-task/edit-writing/edit-writing.component';
import { EditAnalysisComponent } from './administration/task-management/edit-task/edit-analysis/edit-analysis.component';
import { AuthGuardService } from '../shared/guards/auth.guard-service';
import { UserResolverService } from '../shared/resolvers/user.resolver-service';
import { AnalysisTaskComponent } from './tasks/analysis-task/analysis-task.component';
import { EncodePipe } from '../shared/pipes/encode.pipe';
import { AnswerWebService } from './web-services/answer.web-service';
import { TaskAccessGuardService } from '../shared/guards/task-access.guard-service';
import { AnalysisTaskResultComponent } from './tasks/analysis-task/analysis-task-result/analysis-task-result.component';

const routes: Routes = [
  { path: 'administration', component: ChooseManagementComponent, canActivate:[AuthGuardService], resolve:[UserResolverService]},
  { path: 'user', component: UserManagementComponent, canActivate:[AuthGuardService], resolve:[UserResolverService]},
  { path: 'task', component: TaskManagementComponent, canActivate:[AuthGuardService], resolve:[UserResolverService]},
  { path: 'edit-task', component: EditTaskComponent, canActivate:[AuthGuardService], resolve:[UserResolverService] },
  { path: 'edit-task/writing', component: EditWritingComponent, canActivate:[AuthGuardService], resolve:[UserResolverService] },
  { path: 'edit-task/:id', component: EditTaskComponent, canActivate:[AuthGuardService], resolve:[UserResolverService] },
  { path: 'edit-task/:id/writing', component: EditWritingComponent, canActivate:[AuthGuardService],resolve:[UserResolverService] },
  { path: 'analysis-task/:id', component: AnalysisTaskComponent, canActivate:[AuthGuardService, TaskAccessGuardService],resolve:[UserResolverService] },
];

@NgModule({
  declarations: [
    DashboardComponent,
    ChooseDifficultyDialogComponent,
    ChooseManagementComponent,
    UserManagementComponent,
    TaskManagementComponent,
    EditTaskComponent,
    EditWritingComponent,
    EditAnalysisComponent,
    AnalysisTaskComponent,
    EncodePipe,
    AnalysisTaskResultComponent
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
    DragDropModule,
    RouterModule.forRoot(routes)
  ],
  providers: [
    TaskWebService,
    TaskSyncService,
    AnswerWebService
  ],
  entryComponents:[
    ChooseDifficultyDialogComponent,
    AnalysisTaskResultComponent
  ]
})
export class RequirementsModule { }
