import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Tile } from 'src/app/shared/models/tile.model';
import { PageNameSyncService } from 'src/app/shared/services/page-name.sync-service';

@Component({
  selector: 'app-choose-management',
  templateUrl: './choose-management.component.html',
  styleUrls: ['./choose-management.component.scss']
})
export class ChooseManagementComponent implements OnInit {

  tiles: Tile[] = [
    {header: 'User Management', text: 'User Management description User Management description User Management description User Management description', navigation: 'user'},
    {header: 'Task Management', text: 'Task Management description Task Management description Task Management description Task Management description', navigation: 'task'}
  ]

  selectedTabIndex: number;
  constructor(private router: Router,private pageNameService: PageNameSyncService) { }

  ngOnInit(): void {
    this.setPageName();
  }

  private setPageName(){
    this.pageNameService.setPageName("Management selection");
  }

  goToManagement(navigationPart : string) : void{
    this.router.navigate([navigationPart]);
  }
}
