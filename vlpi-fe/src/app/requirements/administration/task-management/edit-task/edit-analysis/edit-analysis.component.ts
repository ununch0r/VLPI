import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TaskSyncService } from 'src/app/requirements/services/task.sync-service';
import { AnalysisTask } from 'src/app/shared/models/analysis-task.model';
import { Requirement } from 'src/app/shared/models/requirement.model';
import { PageNameSyncService } from 'src/app/shared/services/page-name.sync-service';

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
    private router: Router) { }

  ngOnInit(): void {

    this.initForm();
    this.setPageName();
    console.log(this.analysisTask)
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
    let correctRequirements : Requirement[] = formCorrectRequirements.value.map(requirement => ({ description: requirement.description } as Requirement));
    let formWrongRequirements = (<FormArray>this.analysisForm.get('wrongRequirements'));
    let wrongRequirements : Requirement[] = formWrongRequirements.value.map(requirement => ({ description: requirement.description } as Requirement));

    this.analysisTask.description = this.analysisForm.value.description;
    this.analysisTask.correctRequirements = correctRequirements;
    this.analysisTask.wrongRequirements = wrongRequirements;
  }

  showTip(arrayName) : boolean{
    return (<FormArray>this.analysisForm.get(arrayName)).length < 1;
  }

}
