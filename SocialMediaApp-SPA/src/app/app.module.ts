import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms'; // VS Code has no shortcuts for importing angular classes, so must be done manually -_-

import { AppComponent } from './app.component';
import { ValueComponent } from './value/value.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';

@NgModule({
   declarations: [
      AppComponent,
      ValueComponent,
      NavComponent // Right-clicking src > app > generate component has added this to our declarations as well as imports automagically.
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule
   ],
   providers: [ // service providers
     AuthService // Now we can inject this service into our component.
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
