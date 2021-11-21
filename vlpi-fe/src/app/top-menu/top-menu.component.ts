import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../shared/services/auth.service';
import { PageNameSyncService } from '../shared/services/page-name.sync-service';
import { UserSyncService } from '../shared/services/user.sync.service';

@Component({
  selector: 'app-top-menu',
  templateUrl: './top-menu.component.html',
  styleUrls: ['./top-menu.component.scss']
})
export class TopMenuComponent implements OnInit {

  isUserAuthorizedObs: Observable<boolean>;
  constructor(
    private router: Router,
    private pageNameSyncService: PageNameSyncService,
    private userSyncService: UserSyncService,
    private authService: AuthService) { }

  pageNameObs: Observable<string>;
  userNameObs: Observable<string>;

  ngOnInit(): void {
    this.pageNameObs = this.pageNameSyncService.pageNameObs;
    this.userNameObs = this.userSyncService.userNameObs;
    this.isUserAuthorizedObs = this.userSyncService.isUserAuthorizedObs;
  }

  navigateToHome(){
    this.router.navigate(['./']);
  }

  logout(){
    this.userSyncService.setUser(null);
    this.authService.logout();
  }
}
