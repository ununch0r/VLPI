import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { AuthService } from 'src/app/shared/services/auth.service';
import { PageNameSyncService } from 'src/app/shared/services/page-name.sync-service';
import { UserSyncService } from 'src/app/shared/services/user.sync.service';

@Component({
  selector: 'app-authorization',
  templateUrl: './authorization.component.html',
  styleUrls: ['./authorization.component.scss']
})
export class AuthorizationComponent implements OnInit {
  loginForm: FormGroup;  

  constructor(
    private router: Router,
    private authService: AuthService,
    private pageNameService: PageNameSyncService,
    private userSyncService: UserSyncService
  ) { }

  ngOnInit(): void {
    this.initForm();
    this.setPageName();
  }

  private setPageName(){
    this.pageNameService.setPageName("Authorization");
  }
  initForm(){
    this.loginForm = new FormGroup({          
      'login':new FormControl(null,[Validators.email, Validators.required]),
      'password':new FormControl(null, Validators.required)
    })
  }

  onSubmit(){
    this.authService.login(this.loginForm.value.login, this.loginForm.value.password)
    .subscribe({
      next: _ => {
        this.userSyncService.setCurrentUser();
        this.router.navigate([''])
      },
      error: _ => (console.log)
    });
  }

  onRegistration(){
    this.router.navigate(['register'])
  }

}