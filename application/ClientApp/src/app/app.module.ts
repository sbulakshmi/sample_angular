import { AuthService } from './services/auth.service';
import { BrowserXhrWithProgress, ProgressService } from './services/progress.service';
//import { BrowserXhr } from '@angular/common/http/';
import { AppErrorHandler } from './app.error-handler';
import { ErrorHandler } from '@angular/core';
import { VehicleService } from './services/make.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import {ToastyModule} from 'ng2-toasty'
import * as Sentry from "@sentry/browser";

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { VehicleFormComponent } from './vehicle-form/vehicle-form.component';
import { VehicleListComponent } from './vehicle-list/vehicle-list.component';
import { PaginationComponent } from './shared/pagination/pagination.component';
import { ViewVehicleComponent } from './view-vehicle/view-vehicle.component';
import { PhotoService } from './services/photo.service';

import { NavbarComponent } from './navbar/navbar.component';

Sentry.init({
  dsn: "https://6af5d1874afc428aabd28992f7aca402@sentry.io/1497089"
});

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    VehicleFormComponent,
    VehicleListComponent,
    PaginationComponent,
    ViewVehicleComponent,
    NavbarComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ToastyModule.forRoot(),
    FormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo:'vehicles', pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'vehicles', component: VehicleListComponent },
      { path: 'vehicles/new', component: VehicleFormComponent },
      { path: 'vehicles/edit/:id', component: VehicleFormComponent },
      { path: 'vehicles/:id', component: ViewVehicleComponent },
      { path: 'fetch-data', component: FetchDataComponent },
    ])
  ],
  providers: [
    {provide: ErrorHandler, useClass: AppErrorHandler},
   // {provide: BrowserXhr, useClass: BrowserXhrWithProgress},
    VehicleService,
    PhotoService,
    ProgressService,
    AuthService
  ],
    bootstrap: [AppComponent]
})
export class AppModule { }
