import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatButtonModule } from '@angular/material/button';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TopMenuComponent } from './top-menu/top-menu.component';
import { HomeComponent } from './home/home.component';
import { FooterComponent } from './footer/footer.component';
import { RequirementsModule } from './requirements/requirements.module';
import { environment } from 'src/environments/environment';
import { BaseUrlInterceptor } from './shared/interceptors/base-url.interceptor';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { PageNameSyncService } from './shared/services/page-name.sync-service';
import { RegistrationComponent } from './auth/registration/registration.component';
import { AuthorizationComponent } from './auth/authorization/authorization.component';
import { ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';
import { ACCESS_TOKEN_KEY } from './shared/services/auth.service';
import { UserSyncService } from './shared/services/user.sync.service';
import { UserWebService } from './shared/web-services/user.web-service';
import { UserResolverService } from './shared/resolvers/user.resolver-service';
import { MatIconModule } from '@angular/material/icon';
import { ExecutionModeSyncService } from './requirements/services/execution-mode.sycn-service';
import { ExecutionModeResolverService } from './shared/resolvers/execution-mode.resolver-service';
import { UtilsWebService } from './requirements/web-services/utils.web-service';
import { EncodePipe } from './shared/pipes/encode.pipe';
import { DashboardSyncService } from './requirements/services/dashboard.sync-service';

export function tokenGetter(){
  return localStorage.getItem(ACCESS_TOKEN_KEY)
}

@NgModule({
  declarations: [
    AppComponent,
    TopMenuComponent,
    HomeComponent,
    FooterComponent,
    RegistrationComponent,
    AuthorizationComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    CommonModule,
    RequirementsModule,
    MatButtonModule,
    MatGridListModule,
    MatCardModule,
    MatIconModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: ['localhost:44310']
      }
    })
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: BaseUrlInterceptor,
      multi: true,
    },
    { provide: "BASE_API_URL", useValue: environment.apiUrl },
    PageNameSyncService,
    UserSyncService,
    UserWebService,
    UserResolverService,
    ExecutionModeSyncService,
    ExecutionModeResolverService,
    UtilsWebService,
    DashboardSyncService
],
  bootstrap: [AppComponent]
})
export class AppModule { }
