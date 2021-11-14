import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { PageNameSyncService } from '../shared/services/page-name.sync-service';

@Component({
  selector: 'app-top-menu',
  templateUrl: './top-menu.component.html',
  styleUrls: ['./top-menu.component.scss']
})
export class TopMenuComponent implements OnInit {

  constructor(private router: Router, private pageNameSyncService: PageNameSyncService) { }
  pageNameObs: Observable<string>;

  ngOnInit(): void {
    this.pageNameObs = this.pageNameSyncService.pageNameObs;
  }

  navigateToHome(){
    this.router.navigate(['./']);
  }
}
