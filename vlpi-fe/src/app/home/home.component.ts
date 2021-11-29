import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Tile } from '../shared/models/tile.model';
import { PageNameSyncService } from '../shared/services/page-name.sync-service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private router: Router,
     private pageNameService: PageNameSyncService) { }

  ngOnInit(): void {
    this.setPageName();
  }

  private setPageName(){
    this.pageNameService.setPageName("Main dashboard");
  }

  tiles: Tile[] = [
    {header: 'Requirement Analysis', text: 'Test your requirement analysis skills', navigation: '/requirements'},
    {header: 'Design', text: 'Test your design skills', navigation: '/design'},
    {header: 'Modelling', text: 'Test your modelling skills', navigation: '/modelling'},
    {header: 'Coding', text: 'Test your coding skills', navigation: '/coding'},
    {header: 'Testing', text: 'Test your testing skills', navigation: '/testing'},
    ];

    goToModule(navigationPath: string){
      this.router.navigate([navigationPath]);
    }
}
