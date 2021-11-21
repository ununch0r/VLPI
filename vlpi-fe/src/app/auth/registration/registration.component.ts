import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/shared/services/auth.service';
import { PageNameSyncService } from 'src/app/shared/services/page-name.sync-service';
import { UserSyncService } from 'src/app/shared/services/user.sync.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  constructor(
    private router: Router,
    private authService: AuthService,
    private pageNameService: PageNameSyncService,
    private userSyncService: UserSyncService
  ) { }

  ngOnInit(): void {
    this.setPageName();
  }

  private setPageName(){
    this.pageNameService.setPageName("Registration");
  }
}
