import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Tile } from 'src/app/shared/models/tile.model';

@Component({
  selector: 'app-choose-management',
  templateUrl: './choose-management.component.html',
  styleUrls: ['./choose-management.component.scss']
})
export class ChooseManagementComponent implements OnInit {

  managementAreas: Tile[] = [
    {header: null, text: 'User Management', navigation: 'user'},
    {header: null, text: 'Task Management', navigation: 'task'}
  ]

  selectedTabIndex: number = 0;
  constructor(private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
  }

  onTabChanged($event) : void{
    this.router.navigate([this.managementAreas[this.selectedTabIndex].navigation], { relativeTo: this.route });
  }
}
