import { Injectable } from '@angular/core';
import { Subject} from 'rxjs';
import { ShortStatistic } from 'src/app/shared/models/short-statistic.model';
import { UserTaskStatistic } from 'src/app/shared/models/user-task-statistic.model';
import { StatisticWebService } from '../web-services/statistic.web-service';

@Injectable()
export class StatisticSyncService {
  
  private userStatisticSubj = new Subject<UserTaskStatistic[]>();
  userStatisticObs = this.userStatisticSubj.asObservable();
  userStatistic : UserTaskStatistic[];

  private userShortStatisticSubj = new Subject<ShortStatistic[]>();
  userShortStatisticObs = this.userShortStatisticSubj.asObservable();
  userShortStatistic : ShortStatistic[];

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

  reloadShortStatistic(){
    this.statisticWebService.getUserShortStatistic().subscribe(statistic => 
        {
            this.userShortStatisticSubj.next(statistic);
            this.userShortStatistic = statistic;
        });

    return this.userShortStatistic;   
  }
}