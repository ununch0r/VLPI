import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';
import { pipe, Subject, take, takeUntil } from 'rxjs';
import { TaskSyncService } from 'src/app/requirements/services/task.sync-service';
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
export class EditTaskComponent implements OnInit, OnDestroy {
  destroySubj = new Subject();

  id: number;
  createTask: CreateTask
  editMode: boolean = false;
  taskForm : FormGroup;
  types: TaskType[] = [{name: 'Writing requirements', id: 1}, {name: 'Requirements analysis', id:2}];
  submitText: string = 'Submit';

  step: number;
  taskType: number;

  constructor(
    private route: ActivatedRoute,
    private pageNameService: PageNameSyncService,
    private taskSyncService: TaskSyncService
  ) { }

  ngOnInit(): void {
      this.step = 1;
      this.taskType = 1;
      this.trackRouteParams()
      this.setPageName();
      this.setSubmitText();
    }

  trackRouteParams(){
    this.route.params
    .pipe(takeUntil(this.destroySubj))
    .subscribe(
      (params: Params) => {
        this.id = +params['id'];
        this.editMode = params['id'] != null;
        this.initForm();
      })
  }

  private setSubmitText(){
    this.submitText = this.editMode ? 'Update' : 'Next'
  }

  ngOnDestroy(): void {
    this.destroySubj.next('');
    this.destroySubj.complete();
  }
  
  private setPageName(){
    this.pageNameService.setPageName("Task editor");
  }

  private initForm(){
    let objective = '';
    let complexity: number;
    let typeId: number;
    let tips = new FormArray([]);

    if(this.editMode){
      let task = this.taskSyncService.tasks.find(task => task.id === this.id);
      objective = task.objective;
      complexity = task.complexity;
      typeId = task.type.id;

      if(task['taskTip']){
        for(let tip of task.taskTip){
          tips.push(
            new FormGroup({
              'description' : new FormControl(tip.description),
              'order' : new FormControl(tip.order, [Validators.pattern(/^[1-9]+[0-9]*$/)])
            })
          );
        }
      }
    }

    this.taskForm = new FormGroup({
      'objective' : new FormControl(objective, [Validators.required]),
      'complexity' : new FormControl(complexity, [Validators.required]),
      'typeId' : new FormControl({value: typeId, disabled: this.editMode}, [Validators.required]),
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
