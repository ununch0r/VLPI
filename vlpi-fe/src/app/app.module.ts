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

@NgModule({
  declarations: [
    AppComponent,
    TopMenuComponent,
    HomeComponent,
    FooterComponent,
    RegistrationComponent,
    AuthorizationComponent
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
    BrowserAnimationsModule,
    ReactiveFormsModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: BaseUrlInterceptor,
      multi: true,
    },
    { provide: "BASE_API_URL", useValue: environment.apiUrl },
    PageNameSyncService
],
  bootstrap: [AppComponent]
})
export class AppModule { }
