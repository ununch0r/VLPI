import { Component, OnInit } from '@angular/core';
import { PageNameSyncService } from './shared/services/page-name.sync-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'vlpi-fe';
  constructor(private pageNameSyncService : PageNameSyncService){
  }

  ngOnInit(): void {
    this.pageNameSyncService.setPageName("Test");
  }


}
