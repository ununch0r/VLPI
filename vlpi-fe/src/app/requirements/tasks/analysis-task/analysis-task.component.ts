import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Component, OnInit } from '@angular/core';
import { PageNameSyncService } from 'src/app/shared/services/page-name.sync-service';

@Component({
  selector: 'app-analysis-task',
  templateUrl: './analysis-task.component.html',
  styleUrls: ['./analysis-task.component.scss']
})
export class AnalysisTaskComponent implements OnInit {

  rawRequirements = ['Get tosdk;; lfsl;kl ;asfl ;djjl ;fksdajl ;k ;ljkfsl ;jkfdj ;lkfaskj ;dllkj; work', 'Pick up groceries', 'Pick up groceries', 'Pick up groceries', 'Pick up groceries',  'Pick up groceries', 'Pick up groceries', 'Pick up groceries','Pick up groceries', 'Pick up groceries', 'Go home', 'Fall asleep'];
  correctRequirements = ['Get up', 'Brush teeth', 'Take a shower', 'Check e-mail', 'Walk dog'];
  wrongRequirements = ['Get up', 'Brush teeth', 'Take a shower', 'Check e-mail', 'Walk dog'];

  constructor(private pageNameService: PageNameSyncService) { }

  ngOnInit(): void {
    this.setPageName();
  }

  private setPageName(){
    this.pageNameService.setPageName("Requirements analysis task");
  }
  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex,
      );
    }
  }
}
