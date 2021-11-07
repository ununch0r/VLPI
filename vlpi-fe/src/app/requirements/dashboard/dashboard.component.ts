import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';

import { SimpleTask } from 'src/app/shared/models/simple-task.model';
import { Tile } from 'src/app/shared/models/tile.model';
import { ChooseDifficultyDialogComponent } from './choose-difficulty-dialog/choose-difficulty-dialog.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  checked = true;
  constructor(
    private router: Router,
    private dialog: MatDialog
    ) { }

  ngOnInit(): void {
  }


  tiles: Tile[] = [
    {header: 'Statistics', text: 'Check your statistics', navigation: '/statistics'},
    {header: 'Administration', text: 'Manage tasks and users', navigation: '/administration'}
    ];

  tasks: SimpleTask[] =[
    {id:1, order: 1, type: "Writing Requirements"},
    {id:2, order: 2, type: "Requirements Analysis"},
    {id:3, order: 3, type: "Writing Requirements"},
    {id:4, order: 4, type: "Requirements Analysis"},
    {id:5, order: 5, type: "Requirements Analysis"},
    {id:6, order: 6, type: "Writing Requirements"},
    {id:7, order: 7, type: "Writing Requirements"},
  ]

    goToSubModule(navigationPath: string){
      this.router.navigate([navigationPath]);
    }

    chooseDifficulty(id : number){
      const dialogRef = this.dialog.open(ChooseDifficultyDialogComponent);

      dialogRef.afterClosed().subscribe(result => 
        console.log(result)
        );
    }
}
