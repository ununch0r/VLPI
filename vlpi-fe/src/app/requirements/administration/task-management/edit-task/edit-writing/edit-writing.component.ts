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
    private router: Router
    ) { }

  ngOnInit(): void {
    this.initForm();
    this.setPageName();
  }

  private setPageName(){
    this.pageNameService.setPageName("Writing task creation");
  }
  
  initForm() {
    let photoUrl = '';
    let requirements = new FormArray([]);

    this.writingForm = new FormGroup({
      'photoUrl' : new FormControl(photoUrl, [Validators.required]),
      'requirements' : requirements
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

  onSubmit(){
    this.initWritingTask();
    this.taskSyncService.createWritingTask(this.writingTask);
    this.router.navigate(['task'])
  }

  initWritingTask(){
    let formRequirements = (<FormArray>this.writingForm.get('requirements'));
    let requirements : RequirementWithContinuation[] = formRequirements.value.map(requirement => 
      ({ photoUrl: requirement.photoUrl, continuation: requirement.continuation } as RequirementWithContinuation));

    this.writingTask.photoUrl = this.writingForm.value.photoUrl;
    this.writingTask.requirement = requirements;
  }

  showTip(arrayName) : boolean{
    return (<FormArray>this.writingForm.get(arrayName)).length < 1;
  }
}
