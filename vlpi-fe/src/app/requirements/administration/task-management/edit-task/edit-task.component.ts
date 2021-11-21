import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';
import { pipe, Subject, take, takeUntil } from 'rxjs';
import { AnalysisTask } from 'src/app/shared/models/analysis-task.model';
import { CreateTask } from 'src/app/shared/models/create-task.model';
import { TaskTip } from 'src/app/shared/models/task-tip.model';
import { TaskType } from 'src/app/shared/models/task-type.model';
import { PageNameSyncService } from 'src/app/shared/services/page-name.sync-service';

@Component({
  selector: 'app-edit-task',
  templateUrl: './edit-task.component.html',
  styleUrls: ['./edit-task.component.scss']
})
export class EditTaskComponent implements OnInit {
  id: number;
  createTask: CreateTask
  editMode: boolean = false;
  taskForm : FormGroup;
  types: TaskType[] = [{name: 'Writing requirements', id: 1}, {name: 'Requirements analysis', id:2}];

  step: number;
  taskType: number;

  constructor(
    private route: ActivatedRoute,
    private pageNameService: PageNameSyncService
  ) { }

  ngOnInit(): void {
    this.step = 1;
    this.taskType = 0;

    this.route.params
    .subscribe(
      (params: Params) => {
        this.id = +params['id'];
        this.editMode = params['id'] != null;
        this.initForm();
      })
      this.setPageName();
    }
  
  private setPageName(){
    this.pageNameService.setPageName("Task editor");
  }

  private initForm(){
    let objective = '';
    let complexity: number;
    let typeId: number;
    let tips = new FormArray([]);

    //let photoUrl: string;

    this.taskForm = new FormGroup({
      'objective' : new FormControl(objective, [Validators.required]),
      'complexity' : new FormControl(complexity, [Validators.required]),
      'typeId' : new FormControl(typeId, [Validators.required]),
      'tips' : tips
    })
  }

  onDeleteTip(index: number){
    (<FormArray>this.taskForm.get('tips')).removeAt(index);
  }

  onAddTip(){
    let length = (<FormArray>this.taskForm.get('tips')).length;
    (<FormArray>this.taskForm.get('tips')).push(
      new FormGroup({
        'description': new FormControl(''),
        'order': new FormControl(length + 1, [
           Validators.pattern(/^[1-9]+[0-9]*$/)
          ])
      })
    )
  }

  onSubmit(){
    if(this.editMode){

    }else{
      this.initCreateTaskModel();
      this.proccedWithCreation();
    }
  }

  proccedWithCreation(){
    this.step = 2;
    this.taskType = this.taskForm.value.typeId;
  }

  initCreateTaskModel(){
    let formTips = (<FormArray>this.taskForm.get('tips'));
    let taskTips : TaskTip[] = formTips.value.map(tip => ({ description: tip.description, order: tip.order } as TaskTip))
    this.createTask = {
      objective: this.taskForm.value.objective,
      typeId: this.taskForm.value.typeId,
      complexity: this.taskForm.value.complexity,
      taskTip: taskTips
    }
  }

  showTip() : boolean{
    return (<FormArray>this.taskForm.get('tips')).length < 1;
  }

}
