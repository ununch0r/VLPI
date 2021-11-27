import { Injectable } from '@angular/core';
import { Subject} from 'rxjs';
import { ExecutionMode } from 'src/app/shared/models/execution-mode.model';
import { UtilsWebService } from '../web-services/utils.web-service';

@Injectable()
export class ExecutionModeSyncService {
  
  private executionModesSubj = new Subject<ExecutionMode[]>();
  executionModesObs = this.executionModesSubj.asObservable();
  executionModes: ExecutionMode[];

  constructor(private utilsWebService: UtilsWebService) {
      this.executionModesObs.subscribe(executionModes => this.executionModes = executionModes);
  }

  reloadExecutionModes()
  {
    this.utilsWebService.getExecutionModes().subscribe(executionModes => 
        {
            this.executionModesSubj.next(executionModes);
            console.log(executionModes)
        });

    return this.executionModes;
  }  
}