import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TaskSyncService } from 'src/app/requirements/services/task.sync-service';
import { AnalysisTask } from 'src/app/shared/models/analysis-task.model';
import { RequirementWithExplanation } from 'src/app/shared/models/requirement-with-explanation.model';
import { PageNameSyncService } from 'src/app/shared/services/page-name.sync-service';
import { UserSyncService } from 'src/app/shared/services/user.sync.service';

@Component({
  selector: 'app-edit-analysis',
  templateUrl: './edit-analysis.component.html',
  styleUrls: ['./edit-analysis.component.scss']
})
export class EditAnalysisComponent implements OnInit {

  @Input() analysisTask: AnalysisTask
  analysisForm : FormGroup;

  constructor(
    private pageNameService: PageNameSyncService,
    private taskSyncService: TaskSyncService,
    private router: Router
    ) { }

  ngOnInit(): void {
    this.initForm();
    this.setPageName();
  }

  private setPageName(){
    this.pageNameService.setPageName("Analysis task creation");
  }
  
  initForm() {
    let description = '';
    let correctRequirements = new FormArray([]);
    let wrongRequirements = new FormArray([]);

    this.analysisForm = new FormGroup({
      'description' : new FormControl(description, [Validators.required]),
      'correctRequirements' : correctRequirements,
      'wrongRequirements' : wrongRequirements
    })
  }

  onDeleteRequirement(index: number, arrayName: string){
    (<FormArray>this.analysisForm.get(arrayName)).removeAt(index);
  }

  onAddRequirement(arrayName: string){
    (<FormArray>this.analysisForm.get(arrayName)).push(
      new FormGroup({
        'description': new FormControl(''),
        'explanation': new FormControl('') 
      })
    )
  }

  onSubmit(){
    this.initAnalysisTask();
    this.taskSyncService.createAnalysisTask(this.analysisTask);
    this.router.navigate(['task'])
  }

  initAnalysisTask(){
    let formCorrectRequirements = (<FormArray>this.analysisForm.get('correctRequirements'));
    let correctRequirements : RequirementWithExplanation[] = formCorrectRequirements.value.map(requirement =>
       ({ description: requirement.description, isCorrect: true } as RequirementWithExplanation));
    let formWrongRequirements = (<FormArray>this.analysisForm.get('wrongRequirements'));
    let wrongRequirements : RequirementWithExplanation[] = formWrongRequirements.value.map(requirement => 
      ({ description: requirement.description, isCorrect:false, explanation: requirement.explanation } as RequirementWithExplanation));

    this.analysisTask.description = this.analysisForm.value.description;
    this.analysisTask.correctRequirements = correctRequirements;
    this.analysisTask.wrongRequirements = wrongRequirements;
  }

  showTip(arrayName) : boolean{
    return (<FormArray>this.analysisForm.get(arrayName)).length < 1;
  }

}
