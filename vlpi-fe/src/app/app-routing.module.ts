import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { DashboardComponent } from './requirements/dashboard/dashboard.component';
import { AuthorizationComponent } from './auth/authorization/authorization.component';
import { RegistrationComponent } from './auth/registration/registration.component';

const routes: Routes = [
  {path: '',  component: HomeComponent},
  {path: 'requirements', component: DashboardComponent},
  {path: 'auth', component: AuthorizationComponent},
  {path: 'register', component: RegistrationComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
