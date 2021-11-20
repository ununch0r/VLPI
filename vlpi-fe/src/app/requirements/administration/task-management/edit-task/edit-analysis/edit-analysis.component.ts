import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { PageNameSyncService } from 'src/app/shared/services/page-name.sync-service';

@Component({
  selector: 'app-edit-analysis',
  templateUrl: './edit-analysis.component.html',
  styleUrls: ['./edit-analysis.component.scss']
})
export class EditAnalysisComponent implements OnInit {

  analysisForm : FormGroup;

  constructor(private pageNameService: PageNameSyncService) { }

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
      })
    )
  }

  onSubmit(){
    console.log(this.analysisForm.valid);
  }

  showTip(arrayName) : boolean{
    return (<FormArray>this.analysisForm.get(arrayName)).length < 1;
  }

}
