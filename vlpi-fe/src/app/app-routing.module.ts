import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { DashboardComponent } from './requirements/dashboard/dashboard.component';
import { AuthorizationComponent } from './auth/authorization/authorization.component';
import { RegistrationComponent } from './auth/registration/registration.component';
import { AuthGuardService } from './auth/auth-guard.service';
import { UserResolverService } from './shared/resolvers/user-resolver.service';

const routes: Routes = [
  {path: '',  component: HomeComponent, canActivate:[AuthGuardService], resolve:[UserResolverService]},
  {path: 'requirements', component: DashboardComponent, canActivate:[AuthGuardService], resolve:[UserResolverService]},
  {path: 'auth', component: AuthorizationComponent},
  {path: 'register', component: RegistrationComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
