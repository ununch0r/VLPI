import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ExecutionMode } from 'src/app/shared/models/execution-mode.model';
import { RequirementType } from 'src/app/shared/models/requirement-type.model';

const prefix: string = 'util';

@Injectable()
export class UtilsWebService {
  constructor(private http: HttpClient) { }

  getExecutionModes() {
    return this.http.get<ExecutionMode[]>(prefix + '/' + 'execution-modes');
  }

  getRequirementTypes() {
    return this.http.get<RequirementType[]>(prefix + '/' + 'requirement-types');
  }
}