import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SimpleTask } from 'src/app/shared/models/simple-task.model';
import { Tile } from 'src/app/shared/models/tile.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  checked = true;
  constructor(private router: Router) { }

  ngOnInit(): void {
  }


  tiles: Tile[] = [
    {header: 'Statistics', text: 'Check your statistics', navigation: '/statistics'},
    {header: 'Administration', text: 'Manage tasks and users', navigation: '/administration'}
    ];

  tasks: SimpleTask[] =[
    {order: 1, type: "Writing Requirements"},
    {order: 2, type: "Requirements Analysis"},
    {order: 3, type: "Writing Requirements"},
    {order: 4, type: "Requirements Analysis"},
    {order: 5, type: "Requirements Analysis"},
    {order: 6, type: "Writing Requirements"},
    {order: 7, type: "Writing Requirements"},
  ]

    goToSubModule(navigationPath: string){
      this.router.navigate([navigationPath]);
    }
}
