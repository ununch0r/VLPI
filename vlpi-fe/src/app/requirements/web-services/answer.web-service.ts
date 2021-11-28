import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AnalysisTaskAnswer } from 'src/app/shared/models/analysis-task-answer.model';
import { Observable } from 'rxjs';

const prefix: string = 'answer';

@Injectable()
export class AnswerWebService {
  constructor(private http: HttpClient) { }

  createAnalysisTaskAnswer(analysisTaskAnswer: AnalysisTaskAnswer) : Observable<any> {
    return this.http.post(prefix + '/' + 'analysis', analysisTaskAnswer);
  }
}