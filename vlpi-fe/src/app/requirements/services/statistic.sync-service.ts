import { Injectable } from '@angular/core';
import { Subject} from 'rxjs';
import { UserTaskStatistic } from 'src/app/shared/models/user-task-statistic.model';
import { StatisticWebService } from '../web-services/statistic.web-service';

@Injectable()
export class StatisticSyncService {
  
  private userStatisticSubj = new Subject<UserTaskStatistic[]>();
  userStatisticObs = this.userStatisticSubj.asObservable();
  userStatistic : UserTaskStatistic[];

  constructor(private statisticWebService: StatisticWebService) {
  }

  reloadUserStatistic()
  {
    this.statisticWebService.getUserStatistic().subscribe(statistic => 
        {
            this.userStatisticSubj.next(statistic);
            this.userStatistic = statistic;
        });

    return this.userStatistic;
  }  
}