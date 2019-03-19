import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms'; // VS Code has no shortcuts for importing angular classes, so must be done manually -_-

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent, // Right-clicking app>generate component has added this to our declarations/imports automagically.
      HomeComponent,
      RegisterComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule
   ],
   providers: [ // Service Providers
      AuthService // Now we can inject this service into our component.
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
