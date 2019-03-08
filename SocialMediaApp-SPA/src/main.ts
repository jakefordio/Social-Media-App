  // Angular uses Webpack, a module bundler, injects javascript into index.html file on build. (MAGIC)
  // Configuration for Webpack is in Angular.JSON (Contains all the information that Webpack needs to bundle and start our application.)
import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.log(err));
