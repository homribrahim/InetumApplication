import { SignupComponent } from './components/signup/signup.component';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
/* import { DashboardComponent } from './components/dashboard/dashboard.component'; */
/* import { TokenInterceptor } from './interceptors/token.interceptor'; */
import { LandingPageComponent } from './components/landing-page/landing-page.component';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
/* import { NgToastModule } from 'ng-angular-popup'; */
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SignupComponent,
    /* DashboardComponent, */
    LandingPageComponent,
    SpinnerComponent,
    DashboardComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
  ],
  providers: [
    /* {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor, 
      multi: true,
    }, */
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
