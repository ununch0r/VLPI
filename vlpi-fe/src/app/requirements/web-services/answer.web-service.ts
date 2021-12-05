import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AnalysisTaskAnswer } from 'src/app/shared/models/analysis-task-answer.model';
import { Observable } from 'rxjs';
import { AnalysisTaskResult } from 'src/app/shared/models/analysis-task-result.model';
import { WritingTaskAnswer } from 'src/app/shared/models/writing-task-answer.model';

const prefix: string = 'answer';

@Injectable()
export class AnswerWebService {
  constructor(private http: HttpClient) { }

  createAnalysisTaskAnswer(analysisTaskAnswer: AnalysisTaskAnswer) : Observable<AnalysisTaskResult> {
    return this.http.post<AnalysisTaskResult>(prefix + '/' + 'analysis', analysisTaskAnswer);
  }

  createWritingTaskAnswer(writingTaskAnswer: WritingTaskAnswer) : Observable<AnalysisTaskResult> {
    return this.http.post<AnalysisTaskResult>(prefix + '/' + 'writing', writingTaskAnswer);
  }
}