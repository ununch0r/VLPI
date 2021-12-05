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
import { AnalysisTaskResultComponent } from './tasks/analysis-task/analysis-task-result/analysis-task-result.component';
import { SystemStateSyncService } from './services/system-state.sync-service';
import { StatisticsListComponent } from './statistics/statistics-list/statistics-list.component';
import { StatisticWebService } from './web-services/statistic.web-service';
import { StatisticSyncService } from './services/statistic.sync-service';
import { ShortStatisticsComponent } from './statistics/short-statistics/short-statistics.component';
import { StatisticInfoComponent } from './statistics/statistic-info/statistic-info.component';
import { AdminGuardService } from '../shared/guards/admin.guard-service';
import { RequirementTypeResolverService } from '../shared/resolvers/requirement-type.resolver-service';
import { WritingTaskComponent } from './tasks/writing-task/writing-task.component';
import { WritingTaskResultComponent } from './tasks/writing-task/writing-task-result/writing-task-result.component';

const routes: Routes = [
  { path: 'administration', component: ChooseManagementComponent, canActivate:[AuthGuardService, AdminGuardService], resolve:[UserResolverService]},
  { path: 'statistics', component: StatisticsListComponent, canActivate:[AuthGuardService], resolve:[UserResolverService]},
  { path: 'user', component: UserManagementComponent, canActivate:[AuthGuardService], resolve:[UserResolverService]},
  { path: 'task', component: TaskManagementComponent, canActivate:[AuthGuardService], resolve:[UserResolverService]},
  { path: 'edit-task', component: EditTaskComponent, canActivate:[AuthGuardService], resolve:[UserResolverService] },
  { path: 'edit-task/:id', component: EditTaskComponent, canActivate:[AuthGuardService,AdminGuardService], resolve:[UserResolverService] },
  { path: 'analysis-task/:id', component: AnalysisTaskComponent, canActivate:[AuthGuardService], resolve:[UserResolverService] },
  { path: 'writing-task/:id', component: WritingTaskComponent, canActivate:[AuthGuardService], resolve:[UserResolverService] },
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
    AnalysisTaskResultComponent,
    StatisticsListComponent,
    ShortStatisticsComponent,
    StatisticInfoComponent,
    WritingTaskComponent,
    WritingTaskResultComponent
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
    AnswerWebService,
    SystemStateSyncService,
    StatisticWebService,
    StatisticSyncService,
    RequirementTypeResolverService
  ],
  entryComponents:[
    ChooseDifficultyDialogComponent,
    AnalysisTaskResultComponent,
    ShortStatisticsComponent,
    StatisticInfoComponent,
    WritingTaskResultComponent
  ]
})
export class RequirementsModule { }
