import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ExecutionMode } from 'src/app/shared/models/execution-mode.model';
import { UserTaskStatistic } from 'src/app/shared/models/user-task-statistic.model';

const prefix: string = 'statistics';

@Injectable()
export class StatisticWebService {
  constructor(private http: HttpClient) { }

  getUserStatistic() {
    return this.http.get<UserTaskStatistic[]>(prefix + '/' + 'user');
  }
}