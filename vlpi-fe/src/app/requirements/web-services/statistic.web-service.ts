import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ExecutionMode } from 'src/app/shared/models/execution-mode.model';
import { UserTaskStatistic } from 'src/app/shared/models/user-task-statistic.model';
import { ShortStatistic } from 'src/app/shared/models/short-statistic.model';

const prefix: string = 'statistics';

@Injectable()
export class StatisticWebService {
  constructor(private http: HttpClient) { }

  getUserStatistic() {
    return this.http.get<UserTaskStatistic[]>(prefix + '/' + 'user');
  }

  getUserShortStatistic() {
    return this.http.get<ShortStatistic[]>(prefix + '/' + 'user/generic');
  }
}