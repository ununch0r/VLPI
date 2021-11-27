import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ExecutionMode } from 'src/app/shared/models/execution-mode.model';

const prefix: string = 'util';

@Injectable()
export class UtilsWebService {
  constructor(private http: HttpClient) { }

  getExecutionModes() {
    return this.http.get<ExecutionMode[]>(prefix + '/' + 'execution-modes');
  }
}