import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TaskSyncService } from 'src/app/requirements/services/task.sync-service';
import { WritingTask } from 'src/app/shared/models/writing-task.model';
import { PageNameSyncService } from 'src/app/shared/services/page-name.sync-service';
import { RequirementWithContinuation } from 'src/app/shared/models/requirement-with-continuation';

@Component({
  selector: 'app-edit-writing',
  templateUrl: './edit-writing.component.html',
  styleUrls: ['./edit-writing.component.scss']
})
export class EditWritingComponent implements OnInit {

  @Input() writingTask: WritingTask
  writingForm : FormGroup;

  constructor(
    private pageNameService: PageNameSyncService,
    private taskSyncService: TaskSyncService,
    private router: Router,
    ) { }

  ngOnInit(): void {
    this.initForm();
    this.setPageName();
    this.initializeRequirementTypes()
  }

  private initializeRequirementTypes(){
    this.taskSyncService.reloadReqruirementTypes();
  }

  private setPageName(){
    this.pageNameService.setPageName("Writing task creation");
  }
  
  initForm() {
    let photoUrl = '';
    let requirements = new FormArray([]);
    let systemNames = new FormArray([]);

    this.writingForm = new FormGroup({
      'photoUrl' : new FormControl(photoUrl, [Validators.required]),
      'requirements' : requirements,
      'systemNames' : systemNames
    })
  }

  onDeleteRequirement(index: number, arrayName: string){
    (<FormArray>this.writingForm.get(arrayName)).removeAt(index);
  }

  onAddRequirement(arrayName: string){
    (<FormArray>this.writingForm.get(arrayName)).push(
      new FormGroup({
        'description': new FormControl(''),
        'continuation': new FormControl('') 
      })
    )
  }

  onAddSystemName(arrayName: string){
    (<FormArray>this.writingForm.get(arrayName)).push(
      new FormGroup({
        'name': new FormControl(''),
      })
    )
  }

  onSubmit(){
    this.initWritingTask();
    console.log(this.writingTask);
    //this.taskSyncService.createWritingTask(this.writingTask);
    //this.router.navigate(['task'])
  }

  initWritingTask(){
    console.log(this.writingForm);
    let formRequirements = (<FormArray>this.writingForm.get('requirements'));
    let formSystemNames = (<FormArray>this.writingForm.get('systemNames'));

    let requirements : RequirementWithContinuation[] = formRequirements.value.map(requirement => 
      ({ description: requirement.description, continuation: requirement.continuation } as RequirementWithContinuation));
    let systemNames : string[] = formSystemNames.value;

    this.writingTask.photoUrl = this.writingForm.value.photoUrl;
    this.writingTask.requirement = requirements;
    this.writingTask.systemNames = systemNames;
  }

  showTip(arrayName) : boolean{
    return (<FormArray>this.writingForm.get(arrayName)).length < 1;
  }
}
