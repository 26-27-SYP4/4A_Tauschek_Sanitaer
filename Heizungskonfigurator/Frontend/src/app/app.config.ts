import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';

import { BASE_PATH } from './swagger';
import {environment} from '../environments/environment.development'

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    {provide: BASE_PATH, useValue: environment.apiRoot}
  ]
};
