import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
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
  registerForm: FormGroup;

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
    this.registerForm = new FormGroup({          
      email : new FormControl('',[Validators.email, Validators.required]),
      password : new FormControl('', Validators.required),
      confirmPassword : new FormControl('', Validators.required),
      firstName : new FormControl('', Validators.required),
      lastName : new FormControl('', Validators.required)
    }, { validators: this.passwordMatchValidator })
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value
       ? null : {'mismatch': true};
 }


  onSubmit(){
    this.authService.register(this.registerForm.value)
    .subscribe({
      next: _ => {
        this.userSyncService.setCurrentUser();
        this.router.navigate([''])
      },
      error: _ => (console.log)
    });
  }
}
