import {ApplicationConfig, importProvidersFrom} from '@angular/core';
import {provideRouter, RouterModule} from '@angular/router';

import {routes} from './app.routes';
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {HttpClientModule} from "@angular/common/http";

export let appConfig: ApplicationConfig;
appConfig = {
  providers: [
    provideRouter(routes),
    importProvidersFrom(BrowserAnimationsModule, HttpClientModule)
  ]
};
