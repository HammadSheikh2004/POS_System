import {
  ApplicationConfig,
  provideBrowserGlobalErrorListeners,
  provideZonelessChangeDetection,
} from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { Chart, registerables } from 'chart.js';

Chart.register(...registerables)
export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZonelessChangeDetection(),
    provideHttpClient(withFetch()),
    CookieService,
    FontAwesomeModule,
    provideRouter(routes),
  ],
};
