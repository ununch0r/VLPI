import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';
import { TaskType } from 'src/app/shared/models/task-type.model';

@Component({
  selector: 'app-edit-task',
  templateUrl: './edit-task.component.html',
  styleUrls: ['./edit-task.component.scss']
})
export class EditTaskComponent implements OnInit {
  id: number;
  step: number;
  editMode: boolean = false;
  taskForm : FormGroup;
  types: TaskType[] = [{name: 'Writing requirements', id: 1}, {name: 'Requirements analysis', id:2}];


  constructor(
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.step = 1;
    this.route.params.subscribe(
      (params: Params) => {
        this.id = +params['id'];
        this.editMode = params['id'] != null;
        this.initForm();
      })
  }

  private initForm(){
    let objective = '';
    let complexity: number;
    let typeId: number;
    let tips = new FormArray([]);

    //let photoUrl: string;
    //let requirements = new FormArray([]);
    //let description: string;

    this.taskForm = new FormGroup({
      'objective' : new FormControl(objective, [Validators.required]),
      'complexity' : new FormControl(complexity, [Validators.required]),
      //'description' : new FormControl(description),
      'typeId' : new FormControl(null, [Validators.required]),
      'tips' : tips
    })
  }

  onDeleteTip(index: number){
    (<FormArray>this.taskForm.get('tips')).removeAt(index);
  }

  onAddTip(){
    (<FormArray>this.taskForm.get('tips')).push(
      new FormGroup({
        'description': new FormControl(''),
        'order': new FormControl(1, [
           Validators.pattern(/^[1-9]+[0-9]*$/)
          ])
      })
    )
  }

  onSubmit(){
    this.step = 2;
  }

}
