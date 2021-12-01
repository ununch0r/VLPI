import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject} from 'rxjs';

@Injectable()
export class SystemStateSyncService {
  
  private hintsEnabledSubj = new BehaviorSubject<boolean>(false);
  hintsEnabledObs = this.hintsEnabledSubj.asObservable();

  constructor() {
  }

  setHintsEnabledValue(state: boolean){
      this.hintsEnabledSubj.next(state);
  }  
}